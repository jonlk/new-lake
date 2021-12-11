using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using NewLake.Core.GrpcProto;

namespace NewLake.Core.Services.Bulk
{
    public class BulkInfoService
        : NewLakeReceiverService.NewLakeReceiverServiceBase
    {

        private readonly ILogger<BulkInfoService> _logger;

        public BulkInfoService(ILogger<BulkInfoService> logger)
        {
            _logger = logger;
        }

        public override Task<ReturnMessage> SendBulkMessage(InfoMessage request,
                                                            ServerCallContext context)
        {
            var returnMessage = new ReturnMessage
            {
                InfoMessage = "Info about the event"
            };

            return Task.FromResult(returnMessage);
        }
    }
}
