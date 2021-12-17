using System.Linq;
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

        public override Task<ReturnMessage> SendBulkMessage(MessagePacket request, ServerCallContext context)
        {
            var messages = request.InfoMessages.ToList();

            return base.SendBulkMessage(request, context);
        }
    }
}
