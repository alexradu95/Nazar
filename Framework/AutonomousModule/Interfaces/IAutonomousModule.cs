using Nazar.PubSubHub.Hub;
using Nazar.SceneGraph;
using StereoKit.Framework;

namespace Nazar.Framework.Interfaces;

// The IAutonomousModule interface defines the basic structure and properties
// of an autonomous module within the application.
public interface IAutonomousModule
{
    // A unique name for the module
    string Name { get; }

    // The root node of the module in the SceneGraph
    Node? RootNode { get; }

    // The internal MessagingHub for the module to handle its own communication
    MessagingHub? InternalMessagingHub { get; }

    // The external MessagingHub for the module to handle its own communication
    MessagingHub? ExternalMessagingHub { get; }

    // HandMenu Shortcuts commands for this module
    HandRadialLayer? ModuleHandMenuShortcuts { get; }

    // Indicates whether the module is currently enabled or disabled
    // The Step method will run only when IsEnabled is true
    bool IsEnabled { get; set; }

    public void Step();
    public void Shutdown();
}

