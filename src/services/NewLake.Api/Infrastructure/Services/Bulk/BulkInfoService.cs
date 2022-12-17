
namespace NewLake.Api.Infrastructure.Services.Bulk
{
    public class BulkInfoService
        : NewLakeGrpcService.NewLakeGrpcServiceBase
    {
        private readonly ILogger<BulkInfoService> _logger;

        public BulkInfoService(ILogger<BulkInfoService> logger)
        {
            _logger = logger;
        }

        public override async Task<ReturnMessage> ReceiveBulkMessage(MessagePacket request, ServerCallContext context)
        {
            _logger.LogInformation($"Receiving Packet Id: {request.PacketId}");

            await Task.Delay(2000); //simulate some "receiving" ;-)

            _logger.LogInformation($"Processing...");

            foreach (var message in request.InfoMessages)
            {
                _logger.LogInformation($"Message Id: {message.MessageId}, " +
                                        $"Message Guid Data: {message.MessageData}," +
                                        $"Message Time: {message.MessageTime}\n");

                await Task.Delay(1000); //simulate some "processing" ;-)
            }

            _logger.LogInformation($"Completed");

            var returnMessage = new ReturnMessage { ReturnInfo = "Success!" };

            await Task.Delay(2000); //simulate some "tear down" ;-)

            return returnMessage;
        }
    }
}
