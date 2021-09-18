using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NewLake.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();

                    config.AddAzureAppConfiguration(options =>
                    {
                        options.Connect(settings["ConnectionStrings:AppConfig"])
                                .ConfigureKeyVault(kv =>
                                {
                                    var tenantId = settings["AppSettings:TenantId"];
                                    var clientId = settings["AppSettings:ClientId"];
                                    var clientSecret = settings["AppSettings:ClientSecret"];

                                    kv.SetCredential(
                                        new ClientSecretCredential(tenantId, clientId, clientSecret));
                                });
                    });
                });
                webBuilder.UseStartup<Startup>();
            });
    }
}