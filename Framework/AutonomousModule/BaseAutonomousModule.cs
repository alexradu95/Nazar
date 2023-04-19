using Nazar.Framework.Interfaces;
using Nazar.PubSubHub.Hub;
using Nazar.SceneGraph;
using StereoKit.Framework;

namespace Nazar.Framework
{
    public abstract class BaseAutonomousModule : IAutonomousModule
    {
        // The name is required in the inheriting class
        public abstract string Name { get; }

        // Required outside global MessagingHub
        public virtual MessagingHub? ExternalMessagingHub { get; }

        // Optional SceneGraphNode
        public virtual Node? RootNode { get; }

        // Optional MessagingHub
        // If no communication between nodes is needed, there is no need for it
        public virtual MessagingHub? InternalMessagingHub { get; }

        // Optional HandRadialLayer for commands from the hand menu
        public virtual HandRadialLayer? ModuleHandMenuShortcuts { get; }

        // IsEnabled flag, If the IsEnabled flag is true, the Step method will run, otherwise it won't
        public bool IsEnabled { get; set; }

        public virtual void Step()
        {
            if (RootNode != null && IsEnabled) {
                RootNode.Draw();
            }
        }

        public abstract void Shutdown();
    }
}
