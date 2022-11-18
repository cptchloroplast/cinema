using Xunit;
using Movies.SQL.Repositories;
using Movies.SQL.Entities;
using Movies.SQL.Options;
using Movies.SQL.Factories;
using Microsoft.Extensions.Options;
using Moq;
using Movies.Test;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Movies.SQL.Extensions;
using FluentMigrator.Runner;
using System.Data;
namespace Movies.SQL.Test.Repositories;
public class SQLiteMovieRepositoryTest : IDisposable
{
    private readonly IMovieRepository _repo;
    private readonly IDbConnectionFactory _factory;
    private readonly IOptionsMonitor<DbConnectionOptions> _options;
    private readonly IDbConnection _hold;
    public SQLiteMovieRepositoryTest()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var serviceProvider = new ServiceCollection()
            .AddSQLite(configuration)
            .AddSQLiteMigrationRunner(configuration)
            .BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        
        _factory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
        var options = new DbConnectionOptions();
        configuration.Bind(nameof(DbConnectionOptions), options);
        _hold = _factory.CreateConnection(options.ConnectionString);
        _hold.Open();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        _options = Mock.Of<IOptionsMonitor<DbConnectionOptions>>();
        Mock.Get(_options).Setup(x => x.CurrentValue)
            .Returns(options);
        _repo = new SQLiteMovieRepository(_factory, _options);
    }
    public void Dispose()
    {
        _hold.Close();
        _hold.Dispose();
    }
    [Theory]
    [AutoMockData]
    public async Task CRUD(MovieEntity entity)
    {
        // Empty
        var empty = await _repo.Read(entity.TmdbId);
        empty.Should().BeNull();
        // Create
        await _repo.Create(entity);
        var created = await _repo.Read(entity.TmdbId);
        created.Should().NotBe(entity);
        created.Should().BeEquivalentTo(entity);
        // Update
        entity.Title = "new title";
        await _repo.Update(entity);
        var updated = await _repo.Read(entity.TmdbId);
        updated.Should().NotBe(entity);
        updated.Should().BeEquivalentTo(entity);
        // Delete
        await _repo.Delete(entity.TmdbId);
        var deleted = await _repo.Read(entity.TmdbId);
        deleted.Should().BeNull();
    }
}