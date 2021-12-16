using System;
using System.Threading.Tasks;
using NewLake.Core.Domain.Model;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace NewLake.Core
{
    public class CacheService : ICacheService
    {
        private readonly IRedisDatabase _database;

        public CacheService(IRedisCacheClient redisCacheClient)
        {
            _database = redisCacheClient
                .GetDb(0);
        }

        public async Task<CacheItem> SetItemAsync(CacheItem item)
        {
            item.LastUpdated = DateTime.Now;
            await _database.AddAsync(item.Key, item);
            return item;
        }

        public async Task<CacheItem> GetItemAsync(string key)
        {
            var result = await _database.GetAsync<CacheItem>(key);
            return result;
        }

        public async Task<bool> RemoveItemAsync(string key)
        {
            var result = await _database.RemoveAsync(key);
            return result;
        }
    }
}
