using Nazar.PubSubHub.Interfaces;

namespace PubSubHub.PipelineFactory
{
    namespace PubSub
    {
        public interface IPubSubPipelineFactory
        {
            IPublisher GetPublisher();
            ISubscriber GetSubscriber();
        }
    }
}