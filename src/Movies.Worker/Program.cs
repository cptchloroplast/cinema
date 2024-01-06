using Movies.TMDB.Extensions;
using Movies.Commands.Handlers.Extensions;
using Movies.Commands;
using Okkema.Queue.Extensions;
using Okkema.Queue.Producers;
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
            .AddChannelProducer<CreateTMDBMovieV1>();
    })
    .Build();

var services = host.Services;
using var scope = services.CreateScope();
var producer = scope.ServiceProvider.GetRequiredService<IProducer<CreateTMDBMovieV1>>();
for (var i = 1; i <= 500; i++)
{
    await producer.WriteAsync(new CreateTMDBMovieV1 { TmdbId = i });
}

await host.RunAsync();
