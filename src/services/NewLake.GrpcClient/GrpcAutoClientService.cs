using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using NewLake.Core.GrpcProto.Services;
using NewLake.GrpcClient.Sender.Services;

namespace NewLake.GrpcClient
{
    public class GrpcAutoClientService : BackgroundService
    {
        private readonly ILogger<GrpcAutoClientService> _logger;
        private readonly ServiceSettings _serviceSettings;
        private NewLakeGrpcService.NewLakeGrpcServiceClient _client = null;
        private readonly IBulkInfoServiceClient _bulkInfoServiceClient;

        public GrpcAutoClientService(
            IBulkInfoServiceClient bulkInfoServiceClient,
            ILogger<GrpcAutoClientService> logger,
            IOptions<ServiceSettings> options)
        {
            _bulkInfoServiceClient = bulkInfoServiceClient;
            _logger = logger;
            _serviceSettings = options.Value;
        }

        protected NewLakeGrpcService.NewLakeGrpcServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    var channel = GrpcChannel.ForAddress(_serviceSettings.ServerUrl);
                    _client = new NewLakeGrpcService.NewLakeGrpcServiceClient(channel);
                }
                return _client;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int packetId = 1;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Sending Packet Id: {packetId}\n", DateTimeOffset.Now);

                var messageId = _serviceSettings.MessageId;

                var pkt = _bulkInfoServiceClient.BuildMessagePacket(packetId);

                var result = await Client.SendBulkMessageAsync(pkt);

                _logger.LogInformation($"{result.InfoMessage}\n", DateTimeOffset.Now);

                packetId++;
                await Task.Delay(_serviceSettings.DelayInterval, stoppingToken);
            }
        }
    }
}