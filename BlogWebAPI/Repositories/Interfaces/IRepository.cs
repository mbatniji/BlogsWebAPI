using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BlogWebAPI.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Find(object id);
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<int> Add(TEntity entity);
        Task<int> AddRange(IEnumerable<TEntity> entities);

        Task<int> Update(TEntity entity);
        Task<int> UpdateRange(IEnumerable<TEntity> entity);

        Task<int> Remove(TEntity entity);
        Task<int> RemoveRange(IEnumerable<TEntity> entities);
    }
}