namespace NewLake.Api.Infrastructure.Services
{
    public interface ICacheService<TCacheItem> 
        where TCacheItem : CacheItemBase
    {
        Task<TCacheItem> SetAsync(TCacheItem item);
        Task<TCacheItem> GetAsync(string key);
        Task RemoveAsync(string key);
    }
}