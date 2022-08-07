namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly BackgroundHttpClient _backgroundClient;

        public TestController(BackgroundHttpClient backgroundClient)
        {
            _backgroundClient = backgroundClient;
        }

        [HttpGet]
        public ActionResult<string> GetTestValue()
        {
            var result = "Yak successful";
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