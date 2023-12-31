namespace Movies.Commands.Handlers;
public interface ICommandHandler<T> where T : CommandBase
{
    public Task HandleAsync(T command, CancellationToken cancellationToken = default);
}