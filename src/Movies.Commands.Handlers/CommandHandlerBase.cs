using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Movies.Queue.Consumers;
namespace Movies.Commands.Handlers;
public abstract class CommandHandlerBase<T> : BackgroundService, ICommandHandler<T>
    where T : CommandBase
{
    protected readonly ILogger<CommandHandlerBase<T>> _logger;
    private readonly IConsumer<T> _consumer;
    public CommandHandlerBase(
        ILogger<CommandHandlerBase<T>> logger,
        IConsumer<T> consumer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await _consumer.ReadAsync(HandleAsync, cancellationToken);
        }
    }
    public abstract Task HandleAsync(T command, CancellationToken cancellationToken = default);
    
}