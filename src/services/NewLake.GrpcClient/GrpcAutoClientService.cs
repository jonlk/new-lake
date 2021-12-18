using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using NewLake.GrpcClient.Sender.Services;
using static NewLake.Core.GrpcProto.Services.NewLakeGrpcService;

namespace NewLake.GrpcClient
{
    public class GrpcAutoClientService : BackgroundService
    {
        private readonly ILogger<GrpcAutoClientService> _logger;
        private readonly IBulkInfoServiceClient _bulkInfoServiceClient;

        private readonly ServiceSettings _serviceSettings;
        private readonly NewLakeGrpcServiceClient _client;

        public GrpcAutoClientService(
             ILogger<GrpcAutoClientService> logger,
            IBulkInfoServiceClient bulkInfoServiceClient,
            IOptions<ServiceSettings> options)
        {
            _bulkInfoServiceClient = bulkInfoServiceClient;
            _logger = logger;
            _serviceSettings = options.Value;

            if (_client == null)
            {
                var channel = GrpcChannel.ForAddress(_serviceSettings.ServerUrl);
                _client = new NewLakeGrpcServiceClient(channel);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int packetId = 1;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Sending Packet Id: {packetId}", DateTimeOffset.Now);

                var messageId = _serviceSettings.MessageId;

                var pkt = _bulkInfoServiceClient.BuildMessagePacket(packetId);

                try
                {
                    var result = await _client.SendBulkMessageAsync(pkt);

                    _logger.LogInformation($"{result.InfoMessage}", DateTimeOffset.Now);

                    packetId++;

                    await Task.Delay(_serviceSettings.DelayInterval, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"The message packet was not sent " +
                        $"due to an error: {ex.Message}", DateTimeOffset.Now);

                    await StopAsync(stoppingToken);
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Shutting down");
            await base.StopAsync(cancellationToken);
        }
    }
}