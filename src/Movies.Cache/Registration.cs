using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace Movies.Cache;
public static class Registration
{
    public static IServiceCollection AddMemoryCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedMemoryCache();
        return services;
    }
    public static IServiceCollection AddCacheService<T>(this IServiceCollection services,  IConfiguration configuration) where T : class, new()
    {
        services.AddSingleton<CacheSignal<T>>();
        services.AddScoped<ICacheService<T>, CacheService<T>>();
        return services;
    }
}