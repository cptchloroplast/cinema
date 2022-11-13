namespace Movies.Queue;
public interface IConsumer<T>
{
    public Task Read(Func<T, Task> callback);
}