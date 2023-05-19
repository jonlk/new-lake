namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CacheController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("set")]
        public async Task<ActionResult> SetValueAsync([FromBody] AddCacheItemCommand command)
        {            
            var value = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetValueAsync), new { key = value }, null);
        }

        [HttpGet]
        [Route("get/{key}", Name = nameof(GetValueAsync))]
        public async Task<ActionResult<CacheItem>> GetValueAsync(string key)
        {
            var query = new CacheItemQuery { Key = key };
            var result = await _mediator.Send(query);
            if (result == null) { return NotFound($"Item with key {key} not found"); }
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{key}")]
        public async Task<ActionResult<bool>> RemoveValueAsync(string key)
        {
            var command = new RemoveCacheItemCommand { Key = key };
            var result = await _mediator.Send(command);
            return Ok($"Item with key {key} deleted successfully");
        }
    }
}
