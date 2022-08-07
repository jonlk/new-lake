namespace NewLake.Api.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration, ILogger<Startup> logger)
        {
            int retryCount = 3;

            for (int i = 0; i <= retryCount; i++)
            {
                try
                {
                    var redisHost = configuration["RedisHost"].ToString();

                    logger.LogInformation($"Attempting to connect to Redis cache at: {redisHost}");

                    //test for k8s
                    var muxer = ConnectionMultiplexer.Connect($"{redisHost},allowAdmin=true");

                    muxer.GetServer(muxer.GetEndPoints().Single())
                        .ConfigSet("notify-keyspace-events", "Kh");

                    services.AddSingleton<IConnectionMultiplexer>(muxer);
                    services.AddSingleton(typeof(ICacheService<>), typeof(CacheService<>));

                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = muxer.Configuration;
                        //options.InstanceName = "CacheItem:";                
                    });

                    services.AddDistributedMemoryCache();

                    logger.LogInformation($"Successfully connected to Redis cache at: {redisHost}");

                    break;
                }
                catch (Exception ex)
                {
                    if (i < 3)
                    {
                        logger.LogWarning($"Attempt {i}. Could not connect to caching services. Trying again in 5 seconds", ex);
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        logger.LogError($"Failed connecting to caching services. Caching Services Unavailable", ex);
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
    }
}