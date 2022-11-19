namespace Movies.Queue.Producers;
public interface IProducer<T>
{
    public Task Write(T value);
}