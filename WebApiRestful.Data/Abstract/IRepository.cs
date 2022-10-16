using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sample.WebApiRestful.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }

        Task CommitAsync();
        void Delete(Expression<Func<T, bool>> expression);
        void Delete(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetByIdAsync(object id);
        void InserAsynct(T entity);
        void InsertAsync(IEnumerable<T> entities);
        void Update(T entity);
    }
}