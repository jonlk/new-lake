namespace NewLake.Api.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration, ILogger<Startup> logger)
        {
            try
            {
                var redisHost = configuration["RedisHost"].ToString();
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
            }
            catch (Exception ex)
            {
                logger.LogError($"Caching Services Unavailable", ex);
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