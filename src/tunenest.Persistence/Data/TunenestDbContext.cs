using System.Collections.Concurrent;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using tunenest.Domain.Commons;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Domain.Entities;
using tunenest.Domain.Entities.Administrators;
using tunenest.Domain.Entities.Albums;
using tunenest.Domain.Entities.Artists;
using tunenest.Domain.Entities.Playlists;
using tunenest.Domain.Entities.Songs;
using tunenest.Domain.Entities.Users;
using tunenest.Persistence.Data.EntityConfigurations;
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
            modelBuilder.ApplyConfiguration(new AdministratorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumArtistEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistSongEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GenresEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistArtistEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistSongEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistThemeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SongGenresEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SongEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ThemeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserFollowSongEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserFollowAlbumEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserFollowArtistEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserFollowPlaylistEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AdministratorRefreshTokenEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UsersRefreshTokenConfiguration());
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

        #region Generate Dbset

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AlbumArtist> AlbumsArtists { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistSong> ArtistsSongs { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlaylistArtist> PlaylistsArtists { get; set; }
        public virtual DbSet<PlaylistSong> PlaylistsSongs { get; set; }
        public virtual DbSet<PlaylistTheme> PlaylistsThemes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<SongGenres> SongsGenres { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<UserFollowSong> UserFollowSongs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFollowAlbum> UsersFollowAlbums { get; set; }
        public virtual DbSet<UserFollowArtist> UsersFollowArtists { get; set; }
        public virtual DbSet<UserFollowPlaylist> UsersFollowPlaylists { get; set; }
        public virtual DbSet<AdministratorRefreshToken> AdminRefreshTokens { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        #endregion Generate Dbset
    }
}
