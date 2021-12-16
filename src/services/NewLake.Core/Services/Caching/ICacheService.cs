using System.Threading.Tasks;
using NewLake.Core.Domain.Model;

namespace NewLake.Core
{
    public interface ICacheService
    {
        Task<CacheItem> SetItemAsync(CacheItem item);
        Task<CacheItem> GetItemAsync(string key);
        Task<bool> RemoveItemAsync(string key);
    }
}