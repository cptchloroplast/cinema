using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Okkema.Messages.Extensions;
namespace Movies.Commands.Handlers.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMovieCommandHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageHandler<CreateTMDBMovieV1, CreateTMDBMovieV1Handler>();
        return services;
    }
}