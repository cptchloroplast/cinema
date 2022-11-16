using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace Movies.TMDB.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTMDBServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton<ITMDBService, TMDBService>();
        services.AddHttpClient(nameof(TMDBService), client => 
        {  
            client.BaseAddress = new Uri(TMDBOptions.BaseAddress);
        });
        services.AddOptions<TMDBOptions>()
            .Bind(configuration.GetSection(nameof(TMDBOptions)))
            .ValidateDataAnnotations();
        return services;
    }
}