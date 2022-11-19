namespace Movies.Queue.Consumers;
public interface IConsumer<T>
{
    public Task Read(Func<T, CancellationToken, Task> callback, CancellationToken cancellationToken = default);
}