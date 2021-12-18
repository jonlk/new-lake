using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using NewLake.Core.GrpcProto.Services;

namespace NewLake.Core.Services.Bulk
{
    public class BulkInfoService
        : NewLakeGrpcService.NewLakeGrpcServiceBase
    {
        private readonly ILogger<BulkInfoService> _logger;

        public BulkInfoService(ILogger<BulkInfoService> logger)
        {
            _logger = logger;
        }

        public override async Task<ReturnMessage> SendBulkMessage(MessagePacket request, ServerCallContext context)
        {
            _logger.LogInformation($"Receiving Packet Id: {request.PacketId}");

            await Task.Delay(2000);

            _logger.LogInformation($"Processing...");

            await Task.Delay(5000);

            _logger.LogInformation($"Completed");

            var returnMessage = new ReturnMessage { InfoMessage = "Success!" };

            await Task.Delay(2000);

            return returnMessage;
        }
    }
}
