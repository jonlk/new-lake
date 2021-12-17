using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using NewLake.Core.Domain.Model;
using NewLake.Core.Infrastructure;

namespace NewLake.Core
{
    public class CacheService<TCacheItem>
        : ICacheService<TCacheItem>
         where TCacheItem : CacheItemBase
    {
        private readonly IDistributedCache _database;

        public CacheService(IDistributedCache database)
        {
            _database = database;
        }

        public async Task<TCacheItem> SetItemAsync(TCacheItem item)
        {
            item.LastUpdated = DateTime.Now;

            await _database
                .SetAsync(item.Key, item.SerializeToByteArray());

            return item;
        }

        public async Task<TCacheItem> GetItemAsync(string key)
        {
            var bResult = await _database.GetAsync(key);

            var result = ByteArrayExtensions
                .Deserialize<TCacheItem>(bResult);

            return result;
        }

        public async Task RemoveItemAsync(string key)
        {
            await _database.RemoveAsync(key);
        }
    }
}
