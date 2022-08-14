using Microsoft.Extensions.Caching.Distributed;

namespace NewLake.Api.Infrastructure.Services
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

            var existingItem = await GetItemAsync(item.Key);
            
            if (existingItem != null)
            {
                item.PreviousValue = existingItem.Value;
            }

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
