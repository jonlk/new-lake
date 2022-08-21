namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly BackgroundHttpClient _backgroundClient;
        private readonly TestSettings _testSettings;

        public TestController(
            BackgroundHttpClient backgroundClient,
            IOptionsMonitor<TestSettings> options)
        {
            _backgroundClient = backgroundClient;
            _testSettings = options.CurrentValue;
        }

        [HttpGet]
        [Route("string")]
        public ActionResult<string> GetTestValue()
        {
            var result = _testSettings.Name;
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