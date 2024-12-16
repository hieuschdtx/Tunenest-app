using Microsoft.EntityFrameworkCore;
using tunenest.Domain.Commons;
using tunenest.Domain.Commons.Interfaces;
using tunenest.Persistence.Data;

namespace tunenest.Persistence.Repository
{
    public class GenericRepository<T, TEntityId> : IGenericRepository<T, TEntityId> where T : Entity<TEntityId>
    {
        private readonly TunenestDbContext _context;

        public GenericRepository(TunenestDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            return (await _context.Set<T>().AddAsync(entity)).Entity;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TEntityId id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
