using Movies.Worker;
using Movies.TMDB.Extensions;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTMDBServices(configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
