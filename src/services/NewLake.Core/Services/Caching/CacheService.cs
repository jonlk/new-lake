using System;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace NewLake.Core
{
    public class CacheService : ICacheService
    {
        private readonly IRedisDatabase _database;         

        public CacheService(IRedisCacheClient redisCacheClient)
        {
            _database = redisCacheClient
                .GetDbFromConfiguration();            
        }

        public async Task<string> AddItemAsync(string key, string value)
        {
            await _database.AddAsync(key, value, DateTimeOffset.UtcNow.AddHours(6));
            return value;
        }

        public async Task<string> GetItemAsync(string key)
        {
            var result = await _database.GetAsync<string>(key);
            return result;
        }

        public async Task<bool> RemoveItemAsync(string key)
        {
            var result = await _database.RemoveAsync(key);
            return result;
        }
    }
}
