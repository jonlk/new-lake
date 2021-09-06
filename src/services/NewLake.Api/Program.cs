using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Azure.Security.KeyVault.Secrets;
using System;

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
        webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            var settings = config.Build();

            config.AddAzureAppConfiguration(options =>
            {
                options.Connect("https://jkindykv.vault.azure.net/")
                        .ConfigureKeyVault(kv =>
                        {
                            kv.SetCredential(new DefaultAzureCredential(new DefaultAzureCredentialOptions
                            {
                                ManagedIdentityClientId= "7158dad3-9de2-4dbc-be50-41944034e298"
                            }));
                        });
            });
        })
        .UseStartup<Startup>());
    }
}
