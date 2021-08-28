using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewLake.Core;
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

            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
