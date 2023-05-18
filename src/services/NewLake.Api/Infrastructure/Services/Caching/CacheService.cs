public class CacheService : BaseCacheService<CacheItem>, ICacheService<CacheItem>
{
    public CacheService(IDistributedCache database)
        : base(database) { }

    public async override Task<CacheItem> SetAsync(CacheItem item)
    {
        item.LastUpdated = DateTime.Now;

        var existingItem = await GetAsync(item.Key);

        if (existingItem != null)
        {
            item.PreviousValue = existingItem.Value;
            item.Organization.Id = existingItem.Organization.Id;
        }
        else
        {
            item.Organization.Id = Guid.NewGuid();
        }

        return await base.SetAsync(item);
    }

    public async override Task<CacheItem> GetAsync(string key)
    {
        return await base.GetAsync(key);
    }

    public async override Task RemoveAsync(string key)
    {
        await base.RemoveAsync(key);
    }
}