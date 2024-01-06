using Movies.TMDB.Extensions;
using Movies.Commands.Handlers.Extensions;
using Movies.Commands.Movies;
using Movies.Queue.Extensions;
using Movies.Queue.Producers;
using Okkema.SQL.Extensions;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddSQLite(configuration)
            .AddTMDBServices(configuration)
            .AddMovieCommandHandlers(configuration)
            .AddChannelProducer<CreateMovieV1>();
    })
    .Build();

var services = host.Services;
using var scope = services.CreateScope();
var producer = scope.ServiceProvider.GetRequiredService<IProducer<CreateMovieV1>>();
for (var i = 1; i <= 500; i++)
{
    await producer.WriteAsync(new CreateMovieV1 { TmdbId = i });
}

await host.RunAsync();
