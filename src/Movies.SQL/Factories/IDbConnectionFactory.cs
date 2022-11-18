using System.Data;
namespace Movies.SQL.Factories;
public interface IDbConnectionFactory
{
    public IDbConnection CreateConnection(string connectionString);
}