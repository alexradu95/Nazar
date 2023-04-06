using BaseFramework.Core.SceneGraph.Behaviours.Implementations;
using BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces;

namespace BaseFramework.Core.SceneGraph.Nodes.BaseNode
{
    public class Node : INode
    {
        public INodeIdentity NodeIdentity { get; }
        public INodeHierarchy Hierarchy { get; }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public Node(INodeIdentity identity, INodeHierarchy hierarchy)
        {
            NodeIdentity = identity ?? new NodeIdentity();
            Hierarchy = hierarchy ?? new NodeHierarchy();
        }

        public virtual void Step()
        {
            if (!Enabled) return;

            foreach (var child in Hierarchy.Children)
            {
                child.Step();
            }
        }

        /// <inheritdoc/>
        public virtual void Shutdown()
        {
            foreach (var child in Hierarchy.Children)
            {
                child.Shutdown();
            }
        }

        public bool Initialize()
        {
            return true;
        }
    }
}
