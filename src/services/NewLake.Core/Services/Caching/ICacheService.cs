using System.Threading.Tasks;

namespace NewLake.Core
{
    public interface ICacheService
    {
        Task<string> AddItemAsync(string key, string value);
        Task<string> GetItemAsync(string key);
        Task<bool> RemoveItemAsync(string key);
    }
}
