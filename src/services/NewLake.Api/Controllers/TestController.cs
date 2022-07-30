namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController: ControllerBase
    {       
        [HttpGet]       
        public ActionResult<string> GetTestValue()
        {
            var result = "Test successful";
            return Ok(result);
        }
    }
}