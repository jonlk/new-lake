namespace NewLake.Api.Infrastructure.Services
{
    public interface ICacheService<TCacheItem> 
        where TCacheItem : CacheItemBase
    {
        Task<TCacheItem> SetItemAsync(TCacheItem item);
        Task<TCacheItem> GetItemAsync(string key);
        Task RemoveItemAsync(string key);
    }
}