using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sample.WebApiRestful.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sample.WebApiRestful.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        WebApiRestfulContext _sampleWebApiContext;

        public Repository(WebApiRestfulContext sampleWebApiContext)
        {
            _sampleWebApiContext = sampleWebApiContext;
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if(expression == null)
            {
                return await _sampleWebApiContext.Set<T>().ToListAsync();
            }

            return await _sampleWebApiContext.Set<T>().Where(expression).ToListAsync();
        }

        public T GetById(object id)
        {
            return _sampleWebApiContext.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            _sampleWebApiContext.Set<T>().AddAsync(entity);
        }

        public void Insert(IEnumerable<T> entities)
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

            if(entities.Count > 0)
            {
                _sampleWebApiContext.Set<T>().RemoveRange(entities);
            }
        }

        public IQueryable<T> Table => _sampleWebApiContext.Set<T>();

        public async Task Commit()
        {
           await _sampleWebApiContext.SaveChangesAsync();
        }
    }
}
