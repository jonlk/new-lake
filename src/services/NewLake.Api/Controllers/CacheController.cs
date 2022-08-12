namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {        
        private readonly ICacheService<CacheItem> _cacheService;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public CacheController(        
            ICacheService<CacheItem> cacheService,
            IConnectionMultiplexer connectionMultiplexer)
        {     
            _cacheService = cacheService;
            _connectionMultiplexer = connectionMultiplexer;
        }

        [HttpPost]
        [Route("set")]
        public async Task<ActionResult<CacheItem>> SetValueAsync(CacheItem request)
        {  
            var value = await _cacheService
                .SetItemAsync(request);

            await _connectionMultiplexer
                .GetSubscriber()
                .SubscribeAsync("__keyspace@0__:*", async (channel, message) =>
                {
                    Log.Information($"Message: {message} , Channel: {channel}");
                    await _connectionMultiplexer.GetSubscriber().UnsubscribeAllAsync();
                });

            return Ok(request);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<CacheItem>> GetValueAsync(string id)
        {
            var value = await _cacheService.GetItemAsync(id);

            if (value == null) { return NotFound($"Item with key {id} not found"); }

            return Ok(value);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<bool>> RemoveValueAsync(string id)
        {
            await _cacheService.RemoveItemAsync(id);
            return Ok($"Item with key {id} deleted successfully");
        }
    }
}
