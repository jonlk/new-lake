using NewLake.GrpcServer;
using NewLake.GrpcServer.Sender.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<ServiceSettings>(hostContext
            .Configuration
            .GetSection(nameof(ServiceSettings)));

        services.AddSingleton<IBulkPacketGenerator, BulkPacketGenerator>();
        services.AddHostedService<GrpcAutoServer>();
    })
    .Build();

await host.RunAsync();
