using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewLake.Core;

namespace NewLake.Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheService _cacheService;

        public HomeController(
            ILogger<HomeController> logger,
            ICacheService cacheService
            )
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpGet]
        [Route("setvalue")]
        public async Task<ActionResult<string>> SetTestValue()
        {
            var value = await _cacheService
                .AddItemAsync("12", "my test value");

            return Ok($"{value} was set at UTC Time {DateTime.UtcNow}");
        }

        [HttpGet]
        [Route("getvalue")]
        public async Task<ActionResult<string>> GetTestValue()
        {
            var value = await _cacheService
                .GetItemAsync("12");

            if (value == null) { return NotFound("Nothing was found"); }

            return Ok($"{value} was retrieved at UTC Time {DateTime.UtcNow}");
        }

        [HttpDelete]
        [Route("deletevalue")]
        public async Task<ActionResult<bool>> RemoveTestValue()
        {
            var result = await _cacheService
                .RemoveItemAsync("12");

            return Ok(result);
        }
    }
}
