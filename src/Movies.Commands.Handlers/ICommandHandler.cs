namespace Movies.Commands.Handlers;
public interface ICommandHandler<T> where T : CommandBase
{
    public Task HandleCommandAsync(T command, CancellationToken cancellationToken = default);
}