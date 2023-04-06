using Infrastructure.Core.SceneGraph.Behaviours;

namespace BaseFramework.Core.SceneGraph.Behaviours
{
    public interface INodeNamed
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
