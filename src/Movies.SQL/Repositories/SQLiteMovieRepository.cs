using Microsoft.Extensions.Options;
using Movies.SQL.Entities;
using System.Data;
using Dapper;
using Movies.SQL.Options;
using Movies.SQL.Factories;
namespace Movies.SQL.Repositories;
public class SQLiteMovieRepository : IMovieRepository
{
    private readonly IDbConnectionFactory _factory;
    private readonly IOptionsMonitor<DbConnectionOptions> _options;
    public SQLiteMovieRepository(
        IDbConnectionFactory factory,
        IOptionsMonitor<DbConnectionOptions> options)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    private T UseConnection<T>(Func<IDbConnection, T> callback) where T : Task
    {
        using var connection = _factory.CreateConnection(_options.CurrentValue.ConnectionString);
        try
        {
            connection.Open();
            return callback(connection);
        }
        finally
        {
            connection.Close();
        }
    }
    private const string CREATE = 
        @"INSERT INTO Movies(
            TmdbId, 
            Title, 
            CreatedDate, 
            ModifiedDate) 
        VALUES (@TmdbId, @Title, @CreatedDate, @ModifiedDate)";
    public Task Create(MovieEntity entity) => 
        UseConnection<Task>((IDbConnection connection) => 
            connection.ExecuteAsync(CREATE, new 
            { 
                TmdbId = entity.TmdbId, 
                Title = entity.Title, 
                CreatedDate = DateTime.UtcNow, 
                ModifiedDate = DateTime.UtcNow 
            }));
    private const string READ = 
        @"SELECT 
            TmdbId, 
            Title 
        FROM Movies 
        WHERE TmdbId = @TmdbId";
    public Task<MovieEntity> Read(int tmdbId) => 
        UseConnection<Task<MovieEntity>>((IDbConnection connection) => 
            connection.QueryFirstOrDefaultAsync<MovieEntity>(READ, new { TmdbId = tmdbId }));
    private const string UPDATE = 
        @"UPDATE Movies
        SET Title = @Title,
            ModifiedDate = @ModifiedDate
        WHERE TmdbId = @TmdbId";
    public Task Update(MovieEntity entity) => 
        UseConnection<Task>((IDbConnection connection) => 
            connection.ExecuteAsync(UPDATE, new 
            { 
                Title = entity.Title, 
                ModifiedDate = DateTime.UtcNow, 
                TmdbId = entity.TmdbId, 
            }));
    private const string DELETE = 
        @"DELETE FROM Movies
        WHERE TmdbId = @TmdbId";
    public Task Delete(int tmdbId) =>
        UseConnection<Task>((IDbConnection connection) => 
            connection.ExecuteAsync(DELETE, new { TmdbId = tmdbId }));
}