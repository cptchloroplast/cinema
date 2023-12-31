using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Movies.Queue.Extensions;
using Movies.Commands.Handlers.Movies;
using Movies.Commands.Movies;
namespace Movies.Commands.Handlers.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandler<TCommand, THandler>(this IServiceCollection services)
        where TCommand : CommandBase
        where THandler : CommandHandlerBase<TCommand>
    {
        services.AddHostedService<THandler>();
        services.AddSingleton<ICommandHandler<TCommand>, THandler>();
        services.AddChannelConsumer<TCommand>();
        return services;
    }
    public static IServiceCollection AddMovieCommandHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommandHandler<CreateMovieV1, CreateMovieV1Handler>();
        return services;
    }
}