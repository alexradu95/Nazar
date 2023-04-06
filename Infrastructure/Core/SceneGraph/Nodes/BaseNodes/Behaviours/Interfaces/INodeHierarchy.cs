namespace BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces
{
    /// <summary>
    /// Represents a node's hierarchical relationship within a Scene Graph pattern. The Scene Graph is a
    /// hierarchical tree structure that organizes and manages game objects in a 3D graphics engine or
    /// game engine. This interface provides methods and properties to manage parent-child relationships
    /// between nodes.
    /// </summary>
    public interface INodeHierarchy
    {
        /// <summary>
        /// The parent node in the Scene Graph hierarchy.
        /// </summary>
        INode Parent { get; set; }

        /// <summary>
        /// List of child nodes within the Scene Graph hierarchy.
        /// </summary>
        List<INode> Children { get; }

        /// <summary>
        /// Adds a child node to the current node.
        /// </summary>
        /// <param name="child">The child node to add.</param>
        void AddChild(INode child, INode parent);

        /// <summary>
        /// Removes a child node from the current node.
        /// </summary>
        /// <param name="child">The child node to remove.</param>
        void RemoveChild(INode child);

        // Finds a descendant of this node whose name matches nodeName
        INode FindNode(string nodeName, bool recursive = true);

        /// <summary>
        /// Prints the tree of childs in a pretty format
        /// </summary>
        /// <returns></returns>
        string PrintTree();
    }
}