using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLake.Core;
using NewLake.Core.Services.Messaging;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace NewLake.Api.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddStackExchangeRedisExtensions<SystemTextJsonSerializer>((options) =>
            {
                return configuration.GetSection("Redis").Get<RedisConfiguration>();
            });

            var muxer = ConnectionMultiplexer.Connect("localhost,allowAdmin=true");

            muxer.GetServer(muxer.GetEndPoints().Single())
                .ConfigSet("notify-keyspace-events", "K$");

            services.AddSingleton<IConnectionMultiplexer>(muxer);
            services.AddSingleton(typeof(IMessageService<>), typeof(MessageService<>));
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
