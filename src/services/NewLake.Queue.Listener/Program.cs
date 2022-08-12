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
                    var queueConfigSettings = hostContext
                    .Configuration
                    .GetSection("QueueSettings");

                    services.Configure<QueueSettings>(queueConfigSettings);
                    services.AddHostedService<QueueListenerService>();
                })                
                .Build();

await host.RunAsync();