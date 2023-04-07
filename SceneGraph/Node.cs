using SceneGraph.Behaviours;
using SceneGraph.Interfaces;
using StereoKit;

namespace SceneGraph
{
    /// <summary>
    /// A callback function you can register on different node-related events.
    /// </summary>
    /// <param name="node">The node instance the event came from.</param>
    public delegate void NodeEventCallback(Node node);

    /// <summary>
    /// A node with transformations, you can attach renderable entities to it, or append
    /// child nodes to inherit transformations.
    /// </summary>
    public class Node : INode
    {
        private readonly INodeChildren children;
        private readonly IEntityContainer entities;
        private readonly ITransform transform;
        private readonly INodeEvents events;
        private readonly Node parentNode;

        public INodeChildren Children => children;
        public ITransform Transform => transform;
        public INodeEvents Events => events;
        public IEntityContainer Entities => entities;
        public Node Parent { get { return parentNode; } }

        public bool Enabled { get; set; } = true;
        public Guid Id { get; set; }

        public Node(Node parentNode, INodeChildren children, ITransform transform, IEntityContainer entities, INodeEvents events)
        {
            this.children = children ?? new Children();
            this.transform = transform ?? new Transform();
            this.entities = entities ?? new EntityContainer();
            this.events = events ?? new NodeEvents();

            this.parentNode = parentNode;
            this.transform.ParentLastTransformVersion = parentNode != null ? parentNode.Transform.TransformVersion - 1 : 1;
        }

        public void RemoveFromParent()
        {
            if (parentNode == null)
            {
                throw new System.Exception("Can't remove an orphan node from parent.");
            }

            parentNode.Children.RemoveChildNode(this);
        }

        /// <summary>
        /// Draw the node and its children.
        /// </summary>
        public virtual void Draw()
        {
            // not visible? skip
            if (!Enabled)
            {
                return;
            }
            // update transformations (only if needed, testing logic is inside)
            Transform.UpdateTransformations(parentNode);
            // draw all child nodes
            foreach (Node node in Children.ChildrenList)
            {
                node.Draw();
            }
            // draw all child entities
            foreach (IEntity entity in Entities.Entities)
            {
                entity.Draw(this, transform.LocalTransformations, transform.WorldTransformations);
            }
        }

        /// <summary>
        /// Called when local transformations are set, eg when Position, Rotation, Scale etc. is changed.
        /// We use this to set this node as "dirty", eg that we need to update local transformations.
        /// </summary>
        protected virtual void OnTransformationsSet()
        {
            Transform.IsDirty = true;
        }

        /// <summary>
        /// Force update transformations for this node and its children.
        /// </summary>
        /// <param name="recursive">If true, will also iterate and force-update children.</param>
        public void ForceUpdate(bool recursive = true)
        {
            // not visible? skip
            if (!Enabled)
            {
                return;
            }

            // update transformations (only if needed, testing logic is inside)
            Transform.UpdateTransformations(parentNode);

            // force-update all child nodes
            if (recursive)
            {
                foreach (Node node in Children.ChildrenList)
                {
                    node.ForceUpdate(recursive);
                }
            }
        }


    }
}
