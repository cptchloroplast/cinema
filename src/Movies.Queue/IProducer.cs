namespace Movies.Queue;
public interface IProducer<T>
{
    public Task Write(T value);
}