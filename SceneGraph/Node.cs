using SceneGraph;
using StereoKit;

namespace MonoGameSceneGraph
{
    /// <summary>
    /// A callback function you can register on different node-related events.
    /// </summary>
    /// <param name="node">The node instance the event came from.</param>
    public delegate void NodeEventCallback(Node node);

    /// <summary>
    /// A node with transformations, you can attach renderable entities to it, or append child nodes to inherit transformations.
    /// </summary>
    public class Node
    {
        protected Node _parentNode = null;
        public Node ParentNode
        {
            get { return _parentNode; }
            set
            {
                _parentNode = value;
                // set our parents last transformations version to make sure we'll update world transformations next frame.
                _parentLastTransformVersion = value != null ? value._transformVersion - 1 : 1;
            }
        }

        protected List<Node> _childNodes = new();
        protected List<IEntity> _childEntities = new();

        /// <summary>
        /// Callback that triggers every time a node updates its matrix.
        /// </summary>
        public static NodeEventCallback OnTransformationsUpdate;

        /// <summary>
        /// Callback that triggers every time a node is rendered.
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
        /// Return world transformations matrix (note: will recalculate if needed).
        /// </summary>
        public Matrix WorldTransformations
        {
            get
            {
                UpdateTransformations();
                return _worldTransform;
            }
        }

        /// <summary>
        /// Turns true when the transformations of this node changes.
        /// </summary>
        protected bool _isDirty = true;

        /// <summary>
        /// This number increment every time we update transformations.
        /// We use it to check if our parent's transformations had been changed since last
        /// time this node was rendered, and if so, we re-apply parent updated transformations.
        /// </summary>
        protected uint _transformVersion = 0;

        /// <summary>
        /// The last transformations version we got from our parent.
        /// </summary>
        protected uint _parentLastTransformVersion = 0;

        /// <summary>
        /// Transformation version is a special identifier that changes whenever the world transformations
        /// of this node changes. Its not necessarily a sequence, but if you check this number for changes every
        /// frame its a good indication of transformation change.
        /// </summary>
        public uint TransformVersion { get { return _transformVersion; } }

        /// <summary>
        /// Clone this scene node.
        /// </summary>
        /// <returns>Node copy.</returns>
        public virtual Node Clone()
        {
            Node ret = new Node();
            ret._localTransform = _localTransform;
            ret.Visible = Visible;
            return ret;
        }

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

        /// <summary>
        /// Add an entity to this node.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        public void AddEntity(IEntity entity)
        {
            _childEntities.Add(entity);
            OnEntitiesListChange(entity, true);
        }

        /// <summary>
        /// Remove an entity from this node.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        public void RemoveEntity(IEntity entity)
        {
            _childEntities.Remove(entity);
            OnEntitiesListChange(entity, false);
        }

        /// <summary>
        /// Called whenever a child node was added / removed from this node.
        /// </summary>
        /// <param name="entity">Entity that was added / removed.</param>
        /// <param name="wasAdded">If true its an entity that was added, if false, an entity that was removed.</param>
        virtual protected void OnEntitiesListChange(IEntity entity, bool wasAdded)
        {
        }

        /// <summary>
        /// Called whenever an entity was added / removed from this node.
        /// </summary>
        /// <param name="node">Node that was added / removed.</param>
        /// <param name="wasAdded">If true its a node that was added, if false, a node that was removed.</param>
        virtual protected void OnChildNodesListChange(Node node, bool wasAdded)
        {
        }

        /// <summary>
        /// Add a child node to this node.
        /// </summary>
        /// <param name="node">Node to add.</param>
        public void AddChildNode(Node node)
        {
            // node already got a parent?
            if (node._parentNode != null)
            {
                throw new System.Exception("Can't add a node that already have a parent.");
            }

            // add node to children list
            _childNodes.Add(node);

            // set self as node's parent
            node.SetParent(this);
            OnChildNodesListChange(node, true);
        }

        /// <summary>
        /// Remove a child node from this node.
        /// </summary>
        /// <param name="node">Node to add.</param>
        public void RemoveChildNode(Node node)
        {
            // make sure the node is a child of this node
            if (node._parentNode != this)
            {
                throw new System.Exception("Can't remove a node that don't belong to this parent.");
            }

            // remove node from children list
            _childNodes.Remove(node);

            // clear node parent
            node.SetParent(null);
            OnChildNodesListChange(node, false);
        }

        /// <summary>
        /// Find and return first child node by identifier.
        /// </summary>
        /// <param name="identifier">Node identifier to search for.</param>
        /// <param name="searchInChildren">If true, will also search recurisvely in children.</param>
        /// <returns>Node with given identifier or null if not found.</returns>
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

            // if got here it means we didn't find any child node with given identifier
            return null;
        }

        /// <summary>
        /// Remove this node from its parent.
        /// </summary>
        public void RemoveFromParent()
        {
            // don't have a parent?
            if (_parentNode == null)
            {
                throw new System.Exception("Can't remove an orphan node from parent.");
            }

            // remove from parent
            _parentNode.RemoveChildNode(this);
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
            _parentNode?.OnChildWorldMatrixChange(this);
        }

        /// <summary>
        /// Called when local transformations are set, eg when Position, Rotation, Scale etc. is changed.
        /// We use this to set this node as "dirty", eg that we need to update local transformations.
        /// </summary>
        protected virtual void OnTransformationsSet()
        {
            _isDirty = true;
        }

        /// <summary>
        /// Set the parent of this node.
        /// </summary>
        /// <param name="newParent">New parent node to set, or null for no parent.</param>
        protected virtual void SetParent(Node newParent)
        {

        }

        /// <summary>
        /// Return true if we need to update world transform due to parent change.
        /// </summary>
        private bool NeedUpdateDueToParentChange()
        {
            // no parent? if parent last transform version is not 0, it means we had a parent but now we don't. 
            // still require update.
            if (_parentNode == null)
            {
                return _parentLastTransformVersion != 0;
            }

            // check if parent is dirty, or if our last parent transform version mismatch parent current transform version
            return (_parentNode._isDirty || _parentLastTransformVersion != _parentNode._transformVersion);
        }

        /// <summary>
        /// Calc final transformations for current frame.
        /// This uses an indicator to know if an update is needed, so no harm is done if you call it multiple times.
        /// </summary>
        protected virtual void UpdateTransformations()
        {
            // if local transformations are dirty or parent transformations are out-of-date, update world transformations
            if (_isDirty || NeedUpdateDueToParentChange())
            {
                // if we got parent, apply its transformations
                if (_parentNode != null)
                {
                    // if parent need update, update it first
                    if (_parentNode._isDirty)
                    {
                        _parentNode.UpdateTransformations();
                    }

                    // recalc world transform
                    _worldTransform = _localTransform * _parentNode._worldTransform;
                    _parentLastTransformVersion = _parentNode._transformVersion;
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
        /// Get position in world space.
        /// </summary>
        public virtual Vec3 WorldPosition => WorldTransformations.Translation;
        /// <summary>
        /// Get Rotation in world space.
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
        /// Reset all local transformations.
        /// </summary>
        public void ResetTransformations()
        {
            _localTransform = Matrix.Identity;
            OnTransformationsSet();
        }

        /// <summary>
        /// Get / Set node local position.
        /// </summary>
        public Vec3 Position
        {
            get { return _localTransform.Translation; }
            set { if (!_localTransform.Translation.Equals(value)) OnTransformationsSet(); _localTransform.Translation = value; }
        }

        /// <summary>
        /// Get / Set node local scale.
        /// </summary>
        public Vec3 Scale
        {
            get { return _localTransform.Scale; }
            set
            {
                if (!_localTransform.Scale.Equals(value))
                {
                    OnTransformationsSet();
                    _localTransform = _localTransform.Pose.ToMatrix(value);
                }

            }
        }

        /// <summary>
        /// Get / Set node local rotation.
        /// </summary>
        public Quat Rotation
        {
            get { return _localTransform.Rotation; }
            set
            {
                if (!_localTransform.Rotation.Equals(value))
                {
                    OnTransformationsSet();
                    var updatedPoseWithRotation = _localTransform.Pose;
                    updatedPoseWithRotation.orientation = value;
                    _localTransform = updatedPoseWithRotation.ToMatrix();

                }
            }
        }

        /// <summary>
        /// Move position by vector.
        /// </summary>
        /// <param name="moveBy">Vector to translate by.</param>
        public void Translate(Vec3 moveBy)
        {
            _localTransform.Translation += moveBy;
            OnTransformationsSet();
        }

        /// <summary>
        /// Called every time one of the child nodes recalculate world transformations.
        /// </summary>
        /// <param name="node">The child node that updated.</param>
        public virtual void OnChildWorldMatrixChange(Node node)
        {
        }
    }
}