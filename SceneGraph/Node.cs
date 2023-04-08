using SceneGraph.Behaviours;
using SceneGraph.Interfaces;
using StereoKit;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SceneGraph
{
    /// <summary>
    /// A node with transformations, you can attach renderable entities to it, or append
    /// child nodes to inherit transformations.
    /// </summary>
    public class Node : INode
    {
        private readonly INodeChildren _children;
        private readonly IEntityContainer _entities;
        private readonly ITransform _transform;
        private readonly INodeEvents _events;
        private INode _parent;

        public INodeChildren Children => _children;
        public ITransform Transform => _transform;
        public INodeEvents Events => _events;
        public IEntityContainer Entities => _entities;

        public INode Parent
        {
            get => _parent;
            set
            {
                // Remove this node from the current parent's children list
                if (_parent != null)
                {
                    _parent.Children.RemoveChildNode(this);
                }

                // Add this node to the new parent's children list
                if (value != null)
                {
                    value.Children.AddChildNode(this);

                    // Update the node's transformation ParentLastTransformVersion version based on the new parent's transformation
                    // We will substract 1 in order for the Nodes to be updated during the next frame
                    _transform.ParentLastTransformVersion = value.Transform.TransformVersion - 1;
                }
                else
                {
                    // If no new parent is provided, the node's transformation is based on its own local transformation only
                    _transform.ParentLastTransformVersion = 1;
                }

                // Update the parent reference
                _parent = value;
            }
        }

        public bool Enabled { get; set; } = true;
        public Guid Id { get; set; }

        public Node() : this(null, null, null, null)
        {
        }

        public Node(INodeChildren children, ITransform transform, IEntityContainer entities, INodeEvents events)
        {
            _children = children ?? new ChildrenManager();
            _transform = transform ?? new Transform();
            _entities = entities ?? new EntityContainer();
            _events = events ?? new NodeEvents();
        }

        /// <summary>
        /// Draw the node and its children.
        /// </summary>
        public virtual void Draw()
        {
            if (!Enabled)
            {
                return;
            }
            _transform.UpdateTransformations(_parent);
            // draw all child nodes
            foreach (INode node in _children.ChildNodes)
            {
                node.Draw();
            }
            // draw all child entities
            foreach (IEntity entity in _entities.Entities)
            {
                entity.Draw(this, _transform.LocalTransform, _transform.WorldTransform);
            }
        }

        /// <summary>
        /// Force update transformations for this node and its children.
        /// </summary>
        /// <param name="recursive">If true, will also iterate and force-update children.</param>
        public void ForceUpdate(bool recursive = true)
        {
            // not enabled? skip
            if (!Enabled)
            {
                return;
            }

            // update transformations (only if needed, testing logic is inside)
            _transform.UpdateTransformations(_parent);

            // force-update all child nodes
            if (recursive)
            {
                foreach (Node node in _children.ChildNodes)
                {
                    node.ForceUpdate(recursive);
                }
            }
        }
    }
}
