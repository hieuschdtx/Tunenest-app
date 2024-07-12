namespace tunenest.Domain.Commons.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T, TEntityId> Repository<T, TEntityId>() where T : Entity<TEntityId>;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken = default, params string[] cacheKeys);

        void RollbackTransaction();
    }
}
