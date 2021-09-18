using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NewLake.Api
{
    public class Program
    {
        public static void Main(string[] args) { CreateHostBuilder(args).Build().Run(); }

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
                                    kv.SetCredential(
                                        new ClientSecretCredential(
                                            settings["AppSettings:TenantId"],
                                            settings["AppSettings:ClientId"],
                                            settings["AppSettings:ClientSecret"]));
                                });
                    });
                });
                webBuilder.UseStartup<Startup>();
            });
    }
}