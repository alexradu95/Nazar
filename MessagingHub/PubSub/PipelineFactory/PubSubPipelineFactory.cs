using PubSubHub.Hub;
using PubSubHub.PipelineFactory.PubSub;
using PubSubHub.Pub;
using PubSubHub.Pub.Interfaces;
using PubSubHub.Sub;
using PubSubHub.Sub.Interfaces;

namespace PubSub
{
    public class PubSubPipelineFactory : IPubSubPipelineFactory
    {
        private readonly MessagingHub hub;

        public PubSubPipelineFactory()
        {
            hub = new MessagingHub();
        }

        public IPublisher GetPublisher() => new Publisher(hub);

        public ISubscriber GetSubscriber() => new Subscriber(hub);
    }
}