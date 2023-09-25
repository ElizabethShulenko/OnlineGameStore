using Microsoft.EntityFrameworkCore;
using OnlineGameStore.DB.Entities;
using System.Linq.Expressions;

namespace OnlineGameStore.DB.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly OnlineGameStoreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(OnlineGameStoreDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _dbSet.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
