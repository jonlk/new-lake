

using Grpc.Net.Client.Configuration;
using static NewLake.GrpcGenerator.Services.NewLakeGrpcService;

namespace NewLake.GrpcGenerator
{
    public class GrpcAutoServer : BackgroundService
    {
        private readonly ILogger<GrpcAutoServer> _logger;
        private readonly IBulkPacketGenerator _bulkInfoServiceClient;

        private readonly ServiceSettings _serviceSettings;
        private readonly NewLakeGrpcServiceClient _client;

        public GrpcAutoServer(
             ILogger<GrpcAutoServer> logger,
            IBulkPacketGenerator bulkInfoServiceClient,
            IOptions<ServiceSettings> options)
        {
            _bulkInfoServiceClient = bulkInfoServiceClient;
            _logger = logger;
            _serviceSettings = options.Value;

            if (_client == null)
            {
                var channel = GrpcChannel.ForAddress(_serviceSettings.ServerUrl ?? "",
                 new GrpcChannelOptions
                 {
                     Credentials = ChannelCredentials.Insecure,

                     //round robin dispatch test 
                     ServiceConfig = new ServiceConfig
                     {
                         LoadBalancingConfigs = { new RoundRobinConfig() }
                     }
                     
                 });
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

                    _logger.LogInformation($"{result.ReturnInfo}", DateTimeOffset.Now);

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