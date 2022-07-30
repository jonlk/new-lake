namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController: ControllerBase
    {       
        [HttpGet]       
        public ActionResult<string> GetTestValue()
        {
            var result = "Yak successful";
            return Ok(result);
        }
    }
}