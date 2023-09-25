using OnlineGameStore.DB.Entities;
using System.Linq.Expressions;

namespace OnlineGameStore.DB.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task AddAsync(TEntity entity);
        Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
