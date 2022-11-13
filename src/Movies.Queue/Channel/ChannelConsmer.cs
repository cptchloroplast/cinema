using System.Threading.Channels;
namespace Movies.Queue.Channels;
public sealed class ChannelConsumer<T> : IConsumer<T>
{
    private readonly Channel<T> _channel;
    public ChannelConsumer(
        Channel<T> channel)
    {
        _channel = channel ?? throw new ArgumentNullException(nameof(channel));
    }
    public async Task Read(Func<T, Task> callback)
    {
        while (await _channel.Reader.WaitToReadAsync())
        {
            if (_channel.Reader.TryRead(out var value))
            {
                await callback(value);
            }
        }
    }
}