namespace NewLake.GrpcGenerator
{
    public class GrpcGeneratorService : BackgroundService
    {
        private readonly ILogger<GrpcGeneratorService> _logger;
        private readonly IBulkPacketGenerator _bulkInfoServiceClient;

        private readonly ServiceSettings _serviceSettings;
        private readonly NewLakeGrpcServiceClient _client;

        public GrpcGeneratorService(
             ILogger<GrpcGeneratorService> logger,
            IBulkPacketGenerator bulkInfoServiceClient,
            IOptionsMonitor<ServiceSettings> options)
        {
            _bulkInfoServiceClient = bulkInfoServiceClient;
            _logger = logger;
            _serviceSettings = options.CurrentValue;

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
            int retryAttempt = 1;

            while (retryAttempt < _serviceSettings.RetryCount)
            {
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
                    catch (RpcException ex)
                    {
                        _logger.LogError($"The message packet was not sent: " +
                            $"Connection Refused", DateTimeOffset.Now);

                        if (retryAttempt == _serviceSettings.RetryCount)
                        {
                            _logger.LogCritical($"Unable to reach gRPC Server: {_serviceSettings.ServerUrl}");
                            await StopAsync(stoppingToken);
                        }
                        else
                        {
                            _logger.LogInformation($"Attempt {retryAttempt + 1} in {_serviceSettings.RetryInterval / 1000} seconds.");
                            await Task.Delay(_serviceSettings.RetryInterval, stoppingToken);
                        }

                        retryAttempt++;
                    }
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Shutting down");
            Environment.Exit(-1);
            await base.StopAsync(cancellationToken);
        }
    }
}