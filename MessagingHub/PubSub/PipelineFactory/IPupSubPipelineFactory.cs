using PubSubHub.Pub.Interfaces;
using PubSubHub.Sub.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
