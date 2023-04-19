using Nazar.PubSubHub.Hub;
using Nazar.SceneGraph;

namespace Nazar.Framework.Interfaces;

// The IAutonomousModule interface defines the basic structure and properties
// of an autonomous module within the application.
public interface IAutonomousModule
{
    // A unique name for the module
    string Name { get; }

    // The root node of the module in the SceneGraph
    Node RootNode { get; }

    // The internal MessagingHub for the module to handle its own communication
    MessagingHub InternalMessagingHub { get; }

    // Indicates whether the module is currently enabled or disabled
    bool IsEnabled { get; set; }

    public void Step();
    public void Shutdown();
}

