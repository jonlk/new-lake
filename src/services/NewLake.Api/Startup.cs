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
            services.AddGrpc(opt =>
            {
                opt.EnableDetailedErrors = true;
            });         

            services
                .AddCachingServices(_configuration)
                .AddMessagingServices(_configuration)
                .AddBackgroundHttpClient();

            services.AddControllers()
                    .AddFluentValidation(options =>
                    {
                        options.RegisterValidatorsFromAssemblyContaining<CacheItemValidator>();
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"Application started at: {DateTime.UtcNow} UTC");

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
