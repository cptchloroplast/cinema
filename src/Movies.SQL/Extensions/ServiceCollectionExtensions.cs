using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.SQL.Factories;
using Movies.SQL.Options;
using FluentMigrator.Runner;
using System.Reflection;
namespace Movies.SQL.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSQLite(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DbConnectionOptions>()
            .Bind(configuration.GetSection(nameof(DbConnectionOptions)))
            .ValidateDataAnnotations();
        services.AddSingleton<IDbConnectionFactory, SQLiteConnectionFactory>();
        return services;
    }
    public static IServiceCollection AddSQLiteMigrationRunner(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
    {
        var options = new DbConnectionOptions();
        configuration.Bind(nameof(DbConnectionOptions), options);
        services.AddFluentMigratorCore()
            .ConfigureRunner(x => x
                .AddSQLite()
                .WithGlobalConnectionString(options.ConnectionString)
                .WithMigrationsIn(assemblies))
            .AddLogging(x => x.AddFluentMigratorConsole());
        return services;
    }
}