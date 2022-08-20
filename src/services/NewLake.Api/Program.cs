namespace NewLake.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://seq-ingest")
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(options =>
            {
                options.
                    AddJsonFile("config/appsettings.json", 
                        optional: false, 
                        reloadOnChange: true);
            })
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5001, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    });
                    options.Listen(IPAddress.Any, 5002, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
                });

                webBuilder.UseStartup<Startup>();
            });
    }
}
