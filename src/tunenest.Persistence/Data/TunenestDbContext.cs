using System.Collections.Concurrent;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using tunenest.Domain.Commons;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Persistence.Repository;

namespace tunenest.Persistence.Data
{
    public class TunenestDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private IDbContextTransaction _currentTransaction;
        private bool disposed;

        public TunenestDbContext(DbContextOptions<TunenestDbContext> options,
        IDomainEventDispatcher dispatcher) : base(options)
        {
            _dispatcher = dispatcher;
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Count != 0)
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public IGenericRepository<T, TEntityId> Repository<T, TEntityId>() where T : Entity<TEntityId>
        {
            var type = typeof(T);

            if (!_repositories.TryGetValue(type, out var value))
            {
                var repositoryType = typeof(GenericRepository<T, TEntityId>);

                var repositoryInstance = Activator.CreateInstance(repositoryType, this);
                value = repositoryInstance!;
                _repositories[type] = value;
            }

            return (IGenericRepository<T, TEntityId>)value;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(transaction);
            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _currentTransaction;
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
                base.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task<int> SaveAndRemoveCache(CancellationToken cancellationToken = default, params string[] cacheKeys)
        {
            var result = await SaveChangesAsync(cancellationToken);

            // Implement cache removal logic here
            foreach (var cacheKey in cacheKeys)
            {
                // Remove cache by key
            }

            return result;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _currentTransaction?.Dispose();
                    base.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}
