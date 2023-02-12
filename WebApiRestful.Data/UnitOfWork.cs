using System;
using System.Threading.Tasks;
using WebApiRestful.Data.Abstract;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        WebApiRestfulContext _webApiRestfulContext;
        Repository<User> _repositoryUser;
        Repository<UserToken> _repositoryUserToken;

        private bool _disposedValue;

        public UnitOfWork(WebApiRestfulContext webApiRestfulContext)
        {
            _webApiRestfulContext = webApiRestfulContext;
        }

        public Repository<User> RepositoryUser { get { return _repositoryUser ??= new Repository<User>(_webApiRestfulContext); } }
        public Repository<UserToken> RepositoryUserToken
        {
            get
            {
                return _repositoryUserToken ??= new Repository<UserToken>(_webApiRestfulContext);
            }
        }

        public async Task CommitAsync()
        {
            await _webApiRestfulContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _webApiRestfulContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

//Cache

//Memory-Cache -->  IMemoryCache 
//Distribute Cache --> NCache 
//Redis  --> 
//Sql Cache
