using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewLake.Api.Infrastructure.Extensions;
using NewLake.Core.Services.Bulk;

namespace NewLake.Api
{
    public class Startup
    {
        private ILogger<Startup> _logger;
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<Startup>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(opt =>
            {
                opt.EnableDetailedErrors = true;
            });

            services
                .AddCachingServices(_configuration, _logger)
                .AddMessagingServices(_configuration);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _logger.LogInformation($"Application started at: {DateTime.UtcNow} UTC");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BulkInfoService>();

                endpoints.MapControllers();
            });
        }
    }
}
