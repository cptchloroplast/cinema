using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Movies.TMDB.Options;
using Movies.TMDB.Services;
using Movies.TMDB.Repositories;
using Movies.TMDB.Mappers;
namespace Movies.TMDB.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTMDBServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(TMDBProfile));
        services.AddSingleton<ITMDBService, TMDBService>();
        services.AddSingleton<ITMDBMovieRepository, TMDBMovieRepository>();
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