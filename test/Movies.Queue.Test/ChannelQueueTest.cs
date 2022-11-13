using Xunit;
using Movies.Test;
using System.Threading.Channels;
using Movies.Queue;
using Movies.Queue.Channels;
using Moq;
namespace Movies.Cache.Test;
public class ChannelQueueTest
{
    private readonly Channel<MockData> _channel;
    private readonly IProducer<MockData> _producer;
    private readonly IConsumer<MockData> _consumer;
    public ChannelQueueTest()
    {
        _channel = Channel.CreateUnbounded<MockData>();
        _producer = new ChannelProducer<MockData>(_channel);
        _consumer = new ChannelConsumer<MockData>(_channel);
    }
    [Theory]
    [AutoMockData]
    public async Task ProducesAndConsumesMessage(MockData value)
    {
        var callback = Mock.Of<Func<MockData, Task>>();
        _ = Task.Run(() => _consumer.Read(callback));
        await _producer.Write(value);
        await Task.Delay(1000);
        Mock.Get(callback).Verify(x => x(value), Times.Once);
    }
}
