﻿using Sample.WebApiRestful.Data.Abstract;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Service
{
    public class UserService : IUserService
    {
        IRepository<User> _repositoryUser;

        public UserService(IRepository<User> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public async Task<User> CheckLogin(string username, string password)
        {
            return await _repositoryUser.GetSingleByConditionAsync(x => x.Username == username && x.Password == password);
        }
    }
}