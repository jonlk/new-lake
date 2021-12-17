using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewLake.Core.Infrastructure;

namespace NewLake.Queue.Listener
{
    public class Program
    {
        public static void Main(string[] args) { CreateHostBuilder(args).Build().Run(); }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var queueConfigSettings = hostContext
                    .Configuration
                    .GetSection("QueueSettings");

                    services.Configure<QueueSettings>(queueConfigSettings);
                    services.AddHostedService<QueueListenerService>();
                });
    }
}
