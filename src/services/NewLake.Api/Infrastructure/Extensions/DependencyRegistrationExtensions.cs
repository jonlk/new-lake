using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewLake.Core;
using NewLake.Core.Infrastructure;
using NewLake.Core.Services.Messaging;
using StackExchange.Redis;

namespace NewLake.Api.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddCachingServices
            (this IServiceCollection services,
            IConfiguration configuration,
            ILogger<Startup> logger)
        {
            try
            {
                //test for k8s
                var muxer = ConnectionMultiplexer.Connect("localhost,allowAdmin=true");

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