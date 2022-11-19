using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sample.WebApiRestful.Data.Abstract;

namespace Sample.WebApiRestful.Data
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        WebApiRestfulContext _sampleWebApiContext;

        public Repository(WebApiRestfulContext sampleWebApiContext)
        {
            _sampleWebApiContext = sampleWebApiContext;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _sampleWebApiContext.Set<T>().ToListAsync();
            }

            return await _sampleWebApiContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _sampleWebApiContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _sampleWebApiContext.Set<T>().FirstOrDefaultAsync();
        }

        public void InserAsynct(T entity)
        {
            _sampleWebApiContext.Set<T>().AddAsync(entity);
        }

        public void InsertAsync(IEnumerable<T> entities)
        {
            _sampleWebApiContext.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            EntityEntry entityEntry = _sampleWebApiContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            EntityEntry entityEntry = _sampleWebApiContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var entities = _sampleWebApiContext.Set<T>().Where(expression).ToList();

            if (entities.Count > 0)
            {
                _sampleWebApiContext.Set<T>().RemoveRange(entities);
            }
        }

        public IQueryable<T> Table => _sampleWebApiContext.Set<T>();

        public async Task CommitAsync()
        {
            await _sampleWebApiContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            //if (_sampleWebApiContext != null) _sampleWebApiContext.Dispose();
        }
    }
}
