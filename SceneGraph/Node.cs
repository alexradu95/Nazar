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
    public class Node
    {
        /// <summary>
        /// Parent node.
        /// </summary>
        protected Node _parent = null;

        /// <summary>
        /// Callback that triggers every time a node updates its matrix.
        /// </summary>
        public static NodeEventCallback OnTransformationsUpdate;

        /// <summary>
        /// Callback that triggers every time a node is rendered.
        /// Note: nodes that are culled out should not trigger this.
        /// </summary>
        public static NodeEventCallback OnDraw;


        /// <summary>
        /// Is this node currently visible?
        /// </summary>
        public virtual bool Visible { get; set; } = true;

        /// <summary>
        /// Optional identifier we can give to nodes.
        /// </summary>
        public string Identifier;

        /// <summary>
        /// Optional user data we can attach to nodes.
        /// </summary>
        public object UserData;


        /// <summary>
        /// Local transformations matrix, eg the result of the current local transformations.
        /// </summary>
        protected Matrix _localTransform = Matrix.Identity;

        /// <summary>
        /// World transformations matrix, eg the result of the local transformations multiplied with parent transformations.
        /// </summary>
        protected Matrix _worldTransform = Matrix.Identity;

        /// <summary>
        /// Child nodes under this node.
        /// </summary>
        protected List<Node> _childNodes = new List<Node>();

        /// <summary>
        /// Child entities under this node.
        /// </summary>
        protected List<IEntity> _childEntities = new List<IEntity>();

        /// <summary>
        /// Turns true when the transformations of this node changes.
        /// </summary>
        protected bool _isDirty = true;

        /// <summary>
        /// This number increment every time we update transformations.
        /// We use it to check if our parent's transformations had been changed since last
        /// time this node was rendered, and if so, we re-apply parent updated transformations.
        /// </summary>
        protected uint TransformVersion = 0;

        /// <summary>
        /// The last transformations version we got from our parent.
        /// </summary>
        protected uint _parentLastTransformVersion = 0;

        /// <summary>
        /// Get parent node.
        /// </summary>
        public Node Parent { get { return _parent; } }

        /// <summary>
        /// Draw the node and its children.
        /// </summary>
        public virtual void Draw()
        {
            // not visible? skip
            if (!Visible)
            {
                return;
            }
            // update transformations (only if needed, testing logic is inside)
            UpdateTransformations();
            // draw all child nodes
            foreach (Node node in _childNodes)
            {
                node.Draw();
            }
            // trigger draw event
            OnDraw?.Invoke(this);
            // draw all child entities
            foreach (IEntity entity in _childEntities)
            {
                entity.Draw(this, _localTransform, _worldTransform);
            }
        }

        public void AddEntity(IEntity entity)
        {
            _childEntities.Add(entity);
            OnEntitiesListChange(entity, true);
        }

        public void RemoveEntity(IEntity entity)
        {
            _childEntities.Remove(entity);
            OnEntitiesListChange(entity, false);
        }

        virtual protected void OnEntitiesListChange(IEntity entity, bool wasAdded)
        {
        }

        virtual protected void OnChildNodesListChange(Node node, bool wasAdded)
        {
        }

        public void AddChildNode(Node node)
        {
            if (node._parent != null)
            {
                throw new System.Exception("Can't add a node that already have a parent.");
            }
            _childNodes.Add(node);
            node.SetParent(this);
            OnChildNodesListChange(node, true);
        }

        public void RemoveChildNode(Node node)
        {
            if (node._parent != this)
            {
                throw new System.Exception("Can't remove a node that don't belong to this parent.");
            }
            _childNodes.Remove(node);
            node.SetParent(null);
            OnChildNodesListChange(node, false);
        }

        public Node FindChildNode(string identifier, bool searchInChildren = true)
        {
            foreach (Node node in _childNodes)
            {
                // search in direct children
                if (node.Identifier == identifier)
                {
                    return node;
                }

                // recursive search
                if (searchInChildren)
                {
                    Node foundInChild = node.FindChildNode(identifier, searchInChildren);
                    if (foundInChild != null)
                    {
                        return foundInChild;
                    }
                }
            }

            return null;
        }

        public void RemoveFromParent()
        {
            if (_parent == null)
            {
                throw new System.Exception("Can't remove an orphan node from parent.");
            }

            _parent.RemoveChildNode(this);
        }

        /// <summary>
        /// Called when the world matrix of this node is actually recalculated (invoked after the calculation).
        /// </summary>
        protected virtual void OnWorldMatrixChange()
        {
            // update transformations version
            _transformVersion++;

            // trigger update event
            OnTransformationsUpdate?.Invoke(this);

            // notify parent
            if (_parent != null)
            {
                _parent.OnChildWorldMatrixChange(this);
            }
        }

        /// <summary>
        /// Called when local transformations are set, eg when Position, Rotation, Scale etc. is changed.
        /// We use this to set this node as "dirty", eg that we need to update local transformations.
        /// </summary>
        protected virtual void OnTransformationsSet()
        {
            _isDirty = true;
        }

        protected virtual void SetParent(Node newParent)
        {
            // set parent
            _parent = newParent;

            // set our parents last transformations version to make sure we'll update world transformations next frame.
            _parentLastTransformVersion = newParent != null ? newParent._transformVersion - 1 : 1;
        }

        /// <summary>
        /// Return true if we need to update world transform due to parent change.
        /// </summary>
        private bool NeedUpdateDueToParentChange()
        {
            // no parent? if parent last transform version is not 0, it means we had a parent but now we don't. 
            // still require update.
            if (_parent == null)
            {
                return _parentLastTransformVersion != 0;
            }

            // check if parent is dirty, or if our last parent transform version mismatch parent current transform version
            return (_parent._isDirty || _parentLastTransformVersion != _parent._transformVersion);
        }

        /// <summary>
        /// Calc final transformations for current frame.
        /// This uses an indicator to know if an update is needed, so no harm is done if you call it multiple times.
        /// </summary>
        protected virtual void UpdateTransformations()
        {
            // if local transformations are dirty, we need to update them
            if (_isDirty)
            {
                _localTransform = _transformations.BuildMatrix();
            }

            // if local transformations are dirty or parent transformations are out-of-date, update world transformations
            if (_isDirty || NeedUpdateDueToParentChange())
            {
                // if we got parent, apply its transformations
                if (_parent != null)
                {
                    // if parent need update, update it first
                    if (_parent._isDirty)
                    {
                        _parent.UpdateTransformations();
                    }

                    // recalc world transform
                    _worldTransform = _localTransform * _parent._worldTransform;
                    _parentLastTransformVersion = _parent._transformVersion;
                }
                // if not, world transformations are the same as local, and reset parent last transformations version
                else
                {
                    _worldTransform = _localTransform;
                    _parentLastTransformVersion = 0;
                }

                // called the function that mark world matrix change (increase transformation version etc)
                OnWorldMatrixChange();
            }

            // no longer dirty
            _isDirty = false;
        }

        /// <summary>
        /// Return local transformations matrix (note: will recalculate if needed).
        /// </summary>
        public Matrix LocalTransformations
        {
            get { UpdateTransformations(); return _localTransform; }
        }

        /// <summary>
        /// Return world transformations matrix (note: will recalculate if needed).
        /// </summary>
        public Matrix WorldTransformations
        {
            get { UpdateTransformations(); return _worldTransform; }
        }

        /// <summary>
        /// Get position in world space.
        /// </summary>
        /// <remarks>Naive implementation using world matrix decompose. For better performance, override this with your own cached version.</remarks>
        public virtual Vec3 WorldPosition => WorldTransformations.Translation;

        /// <summary>
        /// Get Rotastion in world space.
        /// </summary>
        public virtual Quat WorldRotation => WorldTransformations.Rotation;

        /// <summary>
        /// Get Scale in world space.
        /// </summary>
        public virtual Vec3 WorldScale => WorldTransformations.Scale;

        /// <summary>
        /// Force update transformations for this node and its children.
        /// </summary>
        /// <param name="recursive">If true, will also iterate and force-update children.</param>
        public void ForceUpdate(bool recursive = true)
        {
            // not visible? skip
            if (!Visible)
            {
                return;
            }

            // update transformations (only if needed, testing logic is inside)
            UpdateTransformations();

            // force-update all child nodes
            if (recursive)
            {
                foreach (Node node in _childNodes)
                {
                    node.ForceUpdate(recursive);
                }
            }
        }

        /// <summary>
        /// Called every time one of the child nodes recalculate world transformations.
        /// </summary>
        /// <param name="node">The child node that updated.</param>
        public virtual void OnChildWorldMatrixChange(Node node)
        {
        }

        /// <summary>
        /// Return true if this node is empty.
        /// </summary>
        public bool Empty
        {
            get { return _childEntities.Count == 0 && _childNodes.Count == 0; }
        }

        /// <summary>
        /// Get if this node have any entities in it.
        /// </summary>
        public bool HaveEntities
        {
            get { return _childEntities.Count != 0; }
        }
    }
}