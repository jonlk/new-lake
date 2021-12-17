using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewLake.Queue.Listener.Infrastructure;

namespace NewLake.Queue.Listener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services
                    .Configure<QueueSettings>(hostContext.Configuration.GetSection("QueueSettings"));

                    services.AddHostedService<QueueListenerService>();

                });
    }
}
