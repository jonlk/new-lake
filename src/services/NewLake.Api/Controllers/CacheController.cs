using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewLake.Api.Model;
using NewLake.Core;

namespace NewLake.Api.Controllers
{
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ILogger<CacheController> _logger;
        private readonly ICacheService _cacheService;

        public CacheController(
            ILogger<CacheController> logger,
            ICacheService cacheService)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpPost]
        [Route("set")]
        public async Task<ActionResult<string>> SetValueAsync([FromBody] CacheRequest request)
        {
            var value = await _cacheService
                .AddItemAsync(request.Key, request.Value);

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
