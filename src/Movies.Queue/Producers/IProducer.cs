namespace Movies.Queue.Producers;
public interface IProducer<T>
{
    public Task WriteAsync(T value);
}