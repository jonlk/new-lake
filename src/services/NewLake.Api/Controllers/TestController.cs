

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
        public ActionResult<string> GetTestValue()
        {
            var result = _testSettings.Name;
            return Ok(result);
        }

        [HttpGet]
        [Route("frombackground")]
        public async Task<ActionResult<string>> GetBackgroundTestValue()
        {
            var result = await _backgroundClient.GetResultFromHttpClient();
            return Ok(result);
        }
    }
}