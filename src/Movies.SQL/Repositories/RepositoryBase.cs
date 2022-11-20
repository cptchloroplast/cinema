using Microsoft.Extensions.Options;
using System.Data;
using Movies.SQL.Options;
using Movies.SQL.Factories;
using Movies.SQL.Entities;
using Microsoft.Extensions.Logging;
namespace Movies.SQL.Repositories;
public abstract class RepositoryBase<T> : IRepository<T> 
    where T : EntityBase
{
    protected readonly ILogger<RepositoryBase<T>> _logger;
    private readonly IDbConnectionFactory _factory;
    private readonly IOptionsMonitor<DbConnectionOptions> _options;
    public RepositoryBase(
        ILogger<RepositoryBase<T>> logger,
        IDbConnectionFactory factory,
        IOptionsMonitor<DbConnectionOptions> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    protected TResult UseConnection<TResult>(Func<IDbConnection, TResult> callback) where TResult : Task
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
    public abstract Task Create(T entity);
    public abstract Task<T> Read(Guid key);
    public abstract Task Update(T entity);
    public abstract Task Delete(Guid key);
}