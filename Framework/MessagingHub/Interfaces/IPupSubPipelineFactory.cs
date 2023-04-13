using Framework.MessagingHub.Interfaces;
using PubSubHub.Sub.Interfaces;

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