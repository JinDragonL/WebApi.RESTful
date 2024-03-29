﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiRestful.Data.Abstract;

namespace WebApiRestful.Data
{
    public class Repository<T> : IRepository<T> where T : class
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

        public async Task InsertAsync(T entity)
        {
            await _sampleWebApiContext.Set<T>().AddAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await _sampleWebApiContext.Set<T>().AddRangeAsync(entities);
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

    }
}
