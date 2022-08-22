var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://seq-ingest")
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog(logger)
    .ConfigureServices((hostContext, services) =>
    {    
        services.Configure<ServiceSettings>(hostContext
            .Configuration
            .GetSection(nameof(ServiceSettings)));

        services.AddSingleton<IBulkPacketGenerator, BulkPacketGenerator>();
        services.AddHostedService<GrpcGeneratorService>();
    })
    .Build();

await host.RunAsync();