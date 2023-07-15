namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddCacheItemCommand> _addCacheItemCommandValidator;

        public CacheController(
            IMediator mediator,
            IValidator<AddCacheItemCommand> addCacheItemCommandValidator)
        {
            _mediator = mediator;
            _addCacheItemCommandValidator = addCacheItemCommandValidator;
        }

        [HttpPost]
        [Route("set")]
        public async Task<ActionResult> SetValueAsync([FromBody] AddCacheItemCommand command)
        {
            var validationResult = _addCacheItemCommandValidator.Validate(command);
            if (!validationResult.IsValid) { return BadRequest(validationResult.Errors); }
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
