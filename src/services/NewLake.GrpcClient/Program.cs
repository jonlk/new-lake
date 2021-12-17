using NewLake.GrpcClient;
using NewLake.GrpcClient.Sender.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<ServiceSettings>(hostContext
            .Configuration
            .GetSection(nameof(ServiceSettings)));

        services.AddSingleton<IBulkInfoServiceClient, BulkInfoServiceClient>();
        services.AddHostedService<GrpcAutoClientService>();
    })
    .Build();

await host.RunAsync();
