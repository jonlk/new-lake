namespace NewLake.Api.Infrastructure.Services
{
    public abstract class BaseCacheService<TCacheItem>
        : ICacheService<TCacheItem>
         where TCacheItem : CacheItemBase
    {
        private readonly IDistributedCache _database;

        public BaseCacheService(IDistributedCache database)
        {
            _database = database;
        }

        public async virtual Task<TCacheItem> SetAsync(TCacheItem item)
        {
            await _database
                .SetAsync(item.Key, item.SerializeToByteArray());

            return item;
        }

        public async virtual Task<TCacheItem> GetAsync(string key)
        {
            var bResult = await _database.GetAsync(key);

            var result = ByteArrayExtensions
                .Deserialize<TCacheItem>(bResult);

            return result;
        }

        public async virtual Task RemoveAsync(string key)
        {
            await _database.RemoveAsync(key);
        }
    }
}
