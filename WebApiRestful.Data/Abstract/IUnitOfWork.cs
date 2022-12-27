using Sample.WebApiRestful.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Data.Abstract
{
    public interface IUnitOfWork
    {
        Repository<User> RepositoryUser { get; }
        Repository<UserToken> RepositoryUserToken { get; }

        Task CommitAsync();
    }
}
