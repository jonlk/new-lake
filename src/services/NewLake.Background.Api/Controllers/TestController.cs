using Microsoft.AspNetCore.Mvc;

namespace NewLake.Background.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        _logger.LogInformation($"Received call from front end api client");
        
        var result = "Test Successful, Background Hit!";
        return Ok(result);
    }
}
