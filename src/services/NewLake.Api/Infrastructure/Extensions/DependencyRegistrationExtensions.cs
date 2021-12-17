using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLake.Api.Infrastructure.Settings;
using NewLake.Core;
using NewLake.Core.Services.Messaging;
using StackExchange.Redis;

namespace NewLake.Api.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration)
        {
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
            return services;
        }

        public static IServiceCollection AddMessagingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<QueueSettings>(configuration.GetSection(nameof(QueueSettings)));

            services.AddSingleton(typeof(IMessageService<>), typeof(MessageService<>));
            return services;
        }
    }
}