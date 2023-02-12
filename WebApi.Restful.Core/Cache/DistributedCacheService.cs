using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebApi.Restful.Core.Cache
{
    public class DistributedCacheService : IDistributedCacheService
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Set<T>(string key, T value)
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3),
                SlidingExpiration = TimeSpan.FromMinutes(20)
            });
        }

        public async Task<T> Get<T>(string key)
        {
            string data = await _cache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<T>(data);
            }

            return default;
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}

