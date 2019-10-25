using BlogWebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogWebAPI.Repositories.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly int _takeDefault;
        public Repository(DbContext context)
        {
            Context = context;
            _takeDefault = 20;
        }
        public async Task<TEntity> Find(object id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public async Task<int> Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddRange(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return await Context.SaveChangesAsync();
        }
        public async Task<int> UpdateRange(IEnumerable<TEntity> entity)
        {
            Context.Set<TEntity>().UpdateRange(entity);
            return await Context.SaveChangesAsync();
        }


        public async Task<int> Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            return await Context.SaveChangesAsync();
        }
    }
}