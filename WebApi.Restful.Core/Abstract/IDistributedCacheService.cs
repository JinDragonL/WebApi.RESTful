﻿using System.Threading.Tasks;

namespace WebApi.Restful.Core.Abstract
{
    public interface IDistributedCacheService
    {
        Task<T> Get<T>(string key);
        Task Remove(string key);
        Task Set<T>(string key, T value);
    }
}