using System.Threading.Tasks;
using NewLake.Core.Domain.Model;

namespace NewLake.Core
{
    public interface ICacheService<TCacheItem> 
        where TCacheItem : CacheItemBase
    {
        Task<TCacheItem> SetItemAsync(TCacheItem item);
        Task<TCacheItem> GetItemAsync(string key);
        Task RemoveItemAsync(string key);
    }
}