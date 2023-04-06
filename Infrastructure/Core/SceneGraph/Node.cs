using BaseFramework.Core.SceneGraph.Behaviours;
using Infrastructure.Core.SceneGraph.Behaviours;

namespace BaseFramework.Core.SceneGraph
{
    public abstract class Node : INodeNamed, INodeLifecycle, INodeHierarchy
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public Node Parent { get; private set; }
        public List<Node> Children { get; } = new List<Node>();
        public bool Enabled { get; set; } = true;

        #region Node child management

        /// <inheritdoc/>
        public void AddChild(Node child)
        {
            if (child.Parent != null)
            {
                throw new InvalidOperationException("Child already has a parent.");
            }

            Children.Add(child);
            SetParent(child, this);
        }

        /// <inheritdoc/>
        public void RemoveChild(Node child)
        {
            Children.Remove(child);
            SetParent(child, null);
        }

        // Change the SetParent method to be private.
        private void SetParent(Node child, INodeNamed parent)
        {
            if (child is Node sceneGraphNode)
            {
                sceneGraphNode.Parent = parent;
            }
            else
            {
                throw new InvalidOperationException("The child must be of type SceneGraphNode.");
            }
        }

        #endregion

        #region Lifecycle methods

        /// <inheritdoc/>
        public abstract bool Initialize();

        /// <inheritdoc/>
        public virtual void Step()
        {
            if (!Enabled) return;

            foreach (var child in Children)
            {
                child.Step();
            }
        }

        /// <inheritdoc/>
        public virtual void Shutdown()
        {
            foreach (var child in Children)
            {
                child.Shutdown();
            }
        }

        public INodeNamed FindNode(string nodeName, bool recursive = true)
        {
            throw new NotImplementedException();
        }

        public string PrintTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
