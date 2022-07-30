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

            services.AddControllers()
                    .AddFluentValidation(options =>
                    {
                        options.RegisterValidatorsFromAssemblyContaining<CacheItemValidator>();
                    });
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
