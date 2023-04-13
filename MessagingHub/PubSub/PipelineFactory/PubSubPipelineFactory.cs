using PubSubHub.Hub;
using PubSubHub.PipelineFactory.PubSub;
using PubSubHub.Pub;
using PubSubHub.Pub.Interfaces;
using PubSubHub.Sub;
using PubSubHub.Sub.Interfaces;

namespace PubSub;

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