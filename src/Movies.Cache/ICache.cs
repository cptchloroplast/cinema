namespace Movies.Cache;
public interface ICacheService<T>
{
    public Task<T?> Get(string key, CancellationToken token);
    public Task Set(string key, T value, CancellationToken token);
}
