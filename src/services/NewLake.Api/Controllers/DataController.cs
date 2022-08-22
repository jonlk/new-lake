namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly NewLakeDbContext _newLakeDbContext;
        private readonly IValidator<AddTestDataCommand> _testDataCommandValidator;
        private readonly IMediator _mediator;

        public DataController(
            NewLakeDbContext newLakeDbContext,
            IValidator<AddTestDataCommand> testDataCommandValidator,
            IMediator mediator)
        {
            _newLakeDbContext = newLakeDbContext;
            _testDataCommandValidator = testDataCommandValidator;
            _mediator = mediator;
        }

        [HttpPost]        
        public async Task<ActionResult> CreateDbTestValueAsync([FromBody] AddTestDataCommand command)
        {
            var validationResult = _testDataCommandValidator.Validate(command);
            if (!validationResult.IsValid) { return BadRequest(validationResult.Errors); }
            var result = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetDbTestValueAsync), new { id = result }, null);
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetDbTestValueAsync))]
        public async Task<ActionResult<TestEntity>> GetDbTestValueAsync(string id)
        {
            var query = _newLakeDbContext
                .TestEntities
                .Where(x => x.Id == Guid.Parse(id));

            var result = await query.FirstOrDefaultAsync();

            return Ok(result);
        }
    }
}