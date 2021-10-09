using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewLake.Api.Model;
using NewLake.Core;
using StackExchange.Redis;

namespace NewLake.Api.Controllers
{
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ILogger<CacheController> _logger;
        private readonly ICacheService _cacheService;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public CacheController(
            ILogger<CacheController> logger,
            ICacheService cacheService,
            IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            _cacheService = cacheService;
            _connectionMultiplexer = connectionMultiplexer;
        }

        [HttpPost]
        [Route("set")]
        public async Task<ActionResult<string>> SetValueAsync([FromBody] CacheRequest request)
        {
            var value = await _cacheService
                .AddItemAsync(request.Key, request.Value);

            await _connectionMultiplexer
                .GetSubscriber()
                .SubscribeAsync("__keyspace@0__:*", async (channel, message) =>
                {
                    _logger.LogInformation($"Message: {message} , Channel: {channel}");
                    await _connectionMultiplexer.GetSubscriber().UnsubscribeAllAsync();
                });

            return Ok($"Key:{request.Key} Value:{request.Value} :: {DateTime.UtcNow} UTC");
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<string>> GetValueAsync(string id)
        {
            var value = await _cacheService.GetItemAsync(id);

            if (value == null) { return NotFound($"Item with key {id} not found"); }

            return Ok(value);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<bool>> RemoveValueAsync(string id)
        {
            var result = await _cacheService
                .RemoveItemAsync(id);

            if (!result) { return NotFound($"Item with key {id} not found"); }

            return Ok($"Item with key {id} deleted successfully");
        }
    }
}
