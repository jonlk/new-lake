namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly BackgroundHttpClient _backgroundClient;
        private readonly TestSettings _testSettings;
        private readonly NewLakeDbContext _newLakeDbContext;

        public TestController(
            BackgroundHttpClient backgroundClient,
            IOptionsMonitor<TestSettings> options,
            NewLakeDbContext newLakeDbContext)
        {
            _backgroundClient = backgroundClient;
            _testSettings = options.CurrentValue;
            _newLakeDbContext = newLakeDbContext;
        }

        [HttpGet]
        [Route("string")]
        public ActionResult<string> GetTestValue()
        {
            var result = _testSettings.Name;
            return Ok(result);
        }

        [HttpGet]
        [Route("data/{id}")]
        public async Task<ActionResult<TestEntity>> GetDbTestValueAsync(string id)
        {
            var query = _newLakeDbContext
                .TestEntities
                .Where(x => x.Id == Guid.Parse(id));
            
            var result = await query.FirstOrDefaultAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("frombackground")]
        public async Task<ActionResult<string>> GetBackgroundTestValueAsync()
        {
            var result = await _backgroundClient.GetResultFromHttpClientAsync();
            return Ok(result);
        }
    }
}