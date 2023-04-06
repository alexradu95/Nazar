using BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces;

namespace BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Behaviours
{
    public class NodeHierarchy : INodeHierarchy
    {
        public INode Parent { get; set; }
        public List<INode> Children { get; } = new List<INode>();


        /// <inheritdoc/>
        public void AddChild(INode child, INode parent)
        {
            if (child.Hierarchy.Parent != null)
            {
                throw new InvalidOperationException("Child already has a parent.");
            }

            Children.Add(child);
            SetParent(child, parent);
        }

        /// <inheritdoc/>
        public void RemoveChild(INode child)
        {
            Children.Remove(child);
            SetParent(child, null);
        }

        // Change the SetParent method to be private.
        private void SetParent(INode child, INode parent)
        {
            if (child is Node sceneGraphNode)
            {
                sceneGraphNode.Hierarchy.Parent = parent;
            }
            else
            {
                throw new InvalidOperationException("The child must be of type SceneGraphNode.");
            }
        }

        public INode FindNode(string nodeName, bool recursive = true)
        {
            throw new NotImplementedException();
        }

        public string PrintTree()
        {
            throw new NotImplementedException();
        }

    }
}
