﻿using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Service.Abstract
{
    public interface IUserService
    {
        Task<User> CheckLogin(string username, string password);
        Task<User> FindByUsername(string username);
    }
}