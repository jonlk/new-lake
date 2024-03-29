﻿

namespace NewLake.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddGrpc(opt =>
            {
                opt.EnableDetailedErrors = true;
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            //test settings for ioptionsmonitor
            services
                .Configure<TestSettings>(
                    _configuration.GetSection(nameof(TestSettings)));

            services
                .AddBackgroundHttpClient()
                .AddCachingServices(_configuration)
                .AddDataService(_configuration)
                .AddMessagingServices(_configuration)
                .AddValidationService();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("*");
                });
            });

            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"Application started at: {DateTime.UtcNow} UTC");

            app.AddExceptionHandling(env);

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/readiness");

                endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions()
                {
                    Predicate = (_) => false
                });

                endpoints.MapGrpcService<BulkInfoService>();
                endpoints.MapControllers();
            });
        }
    }
}
