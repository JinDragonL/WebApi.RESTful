﻿using System.Threading.Tasks;
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
