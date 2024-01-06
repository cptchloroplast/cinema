using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Okkema.SQL.Extensions;
using Movies.Migrations;
using System.Reflection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSQLiteMigrationRunner(configuration, new Assembly[] { 
        typeof(AddTmdbMovieTable).Assembly, 
    })
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
try
{
    runner.MigrateUp();
}
catch (Exception exception)
{
    Console.WriteLine(exception);
}
    