namespace NewLake.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {      
        private readonly IMessageService<string> _messageService;

        public MessageController(IMessageService<string> messageService)
        {
            _messageService = messageService;            
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
