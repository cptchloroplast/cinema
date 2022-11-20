using Movies.SQL.Entities;
namespace Movies.SQL.Repositories;
public interface IRepository<T> where T : EntityBase
{
    public Task Create(T entity);
    public Task<T> Read(Guid key);
    public Task Update(T entity);
    public Task Delete(Guid key);
}