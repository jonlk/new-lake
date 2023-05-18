namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService<CacheItem> _cacheService;

        public CacheController(ICacheService<CacheItem> cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpPost]
        [Route("set")]
        public async Task<ActionResult<CacheItem>> SetValueAsync(CacheItem request)
        {   
            var value = await _cacheService
                .SetAsync(request);

            return CreatedAtRoute(nameof(GetValueAsync), new { id = value.Key }, null);
        }

        [HttpGet]
        [Route("get/{id}", Name = nameof(GetValueAsync))]
        public async Task<ActionResult<CacheItem>> GetValueAsync(string id)
        {
            var value = await _cacheService.GetAsync(id);

            if (value == null) { return NotFound($"Item with key {id} not found"); }

            return Ok(value);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<bool>> RemoveValueAsync(string id)
        {
            await _cacheService.RemoveAsync(id);
            return Ok($"Item with key {id} deleted successfully");
        }
    }
}
