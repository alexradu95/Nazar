namespace Infrastructure.Core.SceneGraph.Behaviours
{
    /// <summary>
    /// Represents the lifecycle of a node within a Scene Graph pattern. The Scene Graph is a
    /// hierarchical tree structure that organizes and manages game objects in a 3D graphics engine or
    /// game engine. This interface provides methods for initializing, updating, and shutting down a node.
    /// </summary>
    public interface INodeLifecycle
    {
        /// <summary>
        /// Determines if the node is enabled. When disabled, the node and its children
        /// will not be updated during the Step call.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Initializes the node. This method should be called once before the Step method.
        /// </summary>
        /// <returns>True if the initialization is successful, false otherwise.</returns>
        bool Initialize();

        /// <summary>
        /// Called every frame to update the node's state. This method should be called
        /// after the Initialize method and before the Shutdown method.
        /// </summary>
        void Step();

        /// <summary>
        /// Shuts down the node. This method should be called once after the Step method.
        /// </summary>
        void Shutdown();
    }
}
