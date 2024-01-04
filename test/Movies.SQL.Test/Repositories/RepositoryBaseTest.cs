using Movies.SQL.Repositories;
using Movies.SQL.Entities;
using Movies.SQL.Options;
using Movies.SQL.Factories;
using Microsoft.Extensions.Options;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Movies.SQL.Extensions;
using FluentMigrator.Runner;
using System.Data;
using System.Reflection;
using Movies.Test;
using Xunit;
namespace Movies.SQL.Test.Repositories;
public abstract class RepositoryBaseTest<T> : IDisposable
    where T : EntityBase
{
    protected abstract IRepository<T> Repository { get; }
    protected readonly IDbConnectionFactory _factory;
    protected readonly IOptionsMonitor<DbConnectionOptions> _options;
    private readonly IDbConnection _hold;
    private readonly IMigrationRunner _runner;
    public RepositoryBaseTest(Assembly[] assemblies)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var serviceProvider = new ServiceCollection()
            .AddSQLite(configuration)
            .AddSQLiteMigrationRunner(configuration, assemblies)
            .BuildServiceProvider();
        
        using var scope = serviceProvider.CreateScope();
        var options = new DbConnectionOptions();
        configuration.Bind(nameof(DbConnectionOptions), options);
        _options = Mock.Of<IOptionsMonitor<DbConnectionOptions>>();
        Mock.Get(_options).Setup(x => x.CurrentValue)
            .Returns(options);
        _factory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
        _hold = _factory.CreateConnection(options.ConnectionString);
        _hold.Open();
        _runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        _runner.MigrateUp();
    }
    public void Dispose()
    {
        _hold.Close();
        _hold.Dispose();
    }

    [Theory]
    [AutoMockData]
    public void CRUD(T entity)
    {
        // Empty
        var empty = Repository.Read(entity.SystemKey);
        empty.Should().BeNull();
        // Create
        Repository.Create(entity);
        var created = Repository.Read(entity.SystemKey);
        created.Should().NotBe(entity);
        // Update
        Repository.Update(entity);
        var updated = Repository.Read(entity.SystemKey);
        updated.Should().NotBe(entity);
        // Delete
        Repository.Delete(entity.SystemKey);
        var deleted = Repository.Read(entity.SystemKey);
        deleted.Should().BeNull();
    }
}