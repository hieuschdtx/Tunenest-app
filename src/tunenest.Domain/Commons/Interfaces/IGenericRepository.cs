namespace tunenest.Domain.Commons.Interfaces
{
    public interface IGenericRepository<T, TEntityId> where T : Entity<TEntityId>
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(TEntityId id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
