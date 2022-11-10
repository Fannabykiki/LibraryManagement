using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Data.Repositories.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly BookStoreContext _context;
        public BaseRepository(BookStoreContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            var result = _dbSet.Add(entity).Entity;

            return await Task.FromResult(result);
        }

        public IDatabaseTransaction DatabaseTransaction()
        {
            return new EntityDatabaseTransaction(_context);
        }

        public Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            return Task.FromResult(true);
        }

        public async Task<T>? GetAsync(Expression<Func<T, bool>>? predicate)
        {
            var result = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate)
        {
            var result = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await result.ToListAsync();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate)
        {
            return predicate == null ? _dbSet : _dbSet.Where(predicate);

        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = _dbSet.Update(entity).Entity;

            return await Task.FromResult(result);
        }

    }
}
