using BaseFramework.Core.SceneGraph;
using StereoKit;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System;
using BaseFramework.Core.SceneGraph.Behaviours;

namespace Infrastructure.Core.SceneGraph.Behaviours
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
        INodeHierarchy Parent { get; }

        /// <summary>
        /// List of child nodes within the Scene Graph hierarchy.
        /// </summary>
        List<INodeHierarchy> Children { get; }

        /// <summary>
        /// Adds a child node to the current node.
        /// </summary>
        /// <param name="child">The child node to add.</param>
        void AddChild(INodeHierarchy child);

        /// <summary>
        /// Removes a child node from the current node.
        /// </summary>
        /// <param name="child">The child node to remove.</param>
        void RemoveChild(INodeHierarchy child);

        // Finds a descendant of this node whose name matches nodeName
        INodeHierarchy FindNode(String nodeName, bool recursive = true);

        /// <summary>
        /// Prints the tree of childs in a pretty format
        /// </summary>
        /// <returns></returns>
        string PrintTree();
    }
}