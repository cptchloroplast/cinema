using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Movies.Queue.Consumers;
using Movies.Queue.Producers;
using System.Threading.Channels;
namespace Movies.Queue.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChannelConsumer<T>(this IServiceCollection services)
        where T : class
    {
        services.AddSingleton<IConsumer<T>, ChannelConsumer<T>>();
        services.TryAddSingleton(Channel.CreateUnbounded<T>());
        return services;
    }
    public static IServiceCollection AddChannelProducer<T>(this IServiceCollection services)
        where T : class
    {
        services.AddSingleton<IProducer<T>, ChannelProducer<T>>();
        services.TryAddSingleton(Channel.CreateUnbounded<T>());
        return services;
    }
}