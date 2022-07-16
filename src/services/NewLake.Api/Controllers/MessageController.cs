namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageService<string> _messageService;

        public MessageController(
            ILogger<MessageController> logger,
            IMessageService<string> messageService)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [HttpPost]
        [Route("publish")]
        public ActionResult<string> PublishMessage([FromBody] string request)
        {
            _messageService.Publish(request);

            return Ok();
        }

    }
}
