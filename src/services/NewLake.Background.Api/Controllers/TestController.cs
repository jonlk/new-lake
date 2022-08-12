using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace NewLake.Background.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{

    [HttpGet]
    public ActionResult<string> Get()
    {
        Log.Information($"Received call from front end api client");

        var result = "Test Successful, Background Hit!";
        return Ok(result);
    }
}
