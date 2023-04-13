using Framework.PubSubHub.Hub;
using Framework.PubSubHub.Interfaces;
using PubSubHub.PipelineFactory.PubSub;

namespace Framework.PubSubHub;

public class PubSubPipelineFactory : IPubSubPipelineFactory
{
    private readonly MessagingHub _hub;

    public PubSubPipelineFactory()
    {
        _hub = new MessagingHub();
    }

    public IPublisher GetPublisher()
    {
        return new Publisher(_hub);
    }

    public ISubscriber GetSubscriber()
    {
        return new Subscriber(_hub);
    }
}