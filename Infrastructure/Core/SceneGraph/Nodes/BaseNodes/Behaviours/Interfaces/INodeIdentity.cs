namespace BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces
{
    public interface INodeIdentity
    {
        /// <summary>
        /// Unique identifier for this node.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The name of the node.
        /// </summary>
        string Name { get; }
    }
}
