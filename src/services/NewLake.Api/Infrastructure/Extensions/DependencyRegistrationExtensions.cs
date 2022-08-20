namespace NewLake.Api.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisHost = configuration["RedisHost"].ToString();
            Log.Information($"Attempting to connect to Redis cache at: {redisHost}");

            int retryCount = 3;

            for (int i = 0; i <= retryCount; i++)
            {
                try
                {
                    var muxer = ConnectionMultiplexer.Connect($"{redisHost},allowAdmin=true");

                    muxer.GetServer(muxer.GetEndPoints().Single())
                        .ConfigSet("notify-keyspace-events", "Kh");

                    services.AddSingleton<IConnectionMultiplexer>(muxer);
                    services.AddSingleton(typeof(ICacheService<>), typeof(CacheService<>));

                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = muxer.Configuration;                                    
                    });

                    services.AddDistributedMemoryCache();

                    Log.Information($"Successfully connected to Redis cache at: {redisHost}");

                    break;
                }
                catch (Exception ex)
                {
                    if (i < 3)
                    {
                        Log.Warning($"Attempt {i + 1}. Could not connect to caching services. Trying again in 5 seconds", ex);
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        Log.Error($"Failed connecting to caching services. Caching Services Unavailable", ex);
                    }
                }
            }

            return services;
        }

        public static IServiceCollection AddMessagingServices(this IServiceCollection services, IConfiguration configuration)
        {
            var queueSettings = configuration.GetSection("QueueSettings");

            services.Configure<QueueSettings>(queueSettings);

            services.AddSingleton(typeof(IMessageService<>), typeof(MessageService<>));
            return services;
        }

        public static IServiceCollection AddBackgroundHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<BackgroundHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://new-lake-background-service");
            });

            return services;
        }
    }
}