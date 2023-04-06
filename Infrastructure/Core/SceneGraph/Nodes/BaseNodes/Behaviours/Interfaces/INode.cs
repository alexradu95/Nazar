namespace BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces
{
    public interface INode
    {
        public INodeIdentity NodeIdentity { get; }
        public INodeHierarchy Hierarchy { get; }

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
