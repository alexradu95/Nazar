using MonoGameSceneGraph;
using StereoKit;

namespace SceneGraph
{
    public class Transform
    {
        /// <summary>
        /// Callback that triggers every time a node updates its matrix.
        /// </summary>
        public static NodeEventCallback OnTransformationsUpdate;

        #region Transform versioning

        /// <summary>
        ///     The last transformations version we got from our parent.
        /// </summary>
        public uint ParentLastTransformVersion;

        /// <summary>
        ///     This number increment every time we update transformations.
        ///     We use it to check if our parent's transformations had been changed since last
        ///     time this node was rendered, and if so, we re-apply parent updated transformations.
        ///     Its not necessarily a sequence, but if you check this number for changes every
        ///     frame its a good indication of transformation change.
        /// </summary>
        public uint TransformVersion;
        #endregion

        #region World and Local Transforms

        /// <summary>
        ///     Return world transformations matrix (note: will recalculate if needed).
        /// </summary>
        public Matrix WorldTransformations
        {
            get
            {
                UpdateTransformations();
                return worldTransform;
            }
        }

        /// <summary>
        ///     Return local transformations matrix (note: will recalculate if needed).
        /// </summary>
        public Matrix LocalTransformations
        {
            get
            {
                UpdateTransformations();
                return localTransform;
            }
        }

        #endregion

        #region Position, Scale, Rotation

        /// <summary>
        ///     Get / Set node local position.
        /// </summary>
        public Vec3 Position
        {
            get => localTransform.Translation;
            set
            {
                if (!localTransform.Translation.Equals(value)) OnTransformationsSet();
                localTransform.Translation = value;
            }
        }

        /// <summary>
        ///     Get / Set node local scale.
        /// </summary>
        public Vec3 Scale
        {
            get => localTransform.Scale;
            set
            {
                if (localTransform.Scale.Equals(value)) return;
                OnTransformationsSet();
                localTransform = localTransform.Pose.ToMatrix(value);
            }
        }

        /// <summary>
        ///     Get / Set node local rotation.
        /// </summary>
        public Quat Rotation
        {
            get => localTransform.Rotation;
            set
            {
                if (localTransform.Rotation.Equals(value)) return;

                OnTransformationsSet();
                Pose updatedPoseWithRotation = localTransform.Pose;
                updatedPoseWithRotation.orientation = value;
                localTransform = updatedPoseWithRotation.ToMatrix();
            }
        }

        #endregion

        #region References to the owner node and it's parent

        private Node currentNode;
        private Node parentNode => currentNode.ParentNode;

        #endregion

        public Transform(Node ownerNode)
        {
            this.currentNode = ownerNode;
        }

        /// <summary>
        ///     Turns true when the transformations of this node changes.
        /// </summary>
        public bool _isDirty = true;

        /// <summary>
        ///     Called when local transformations are set, eg when Position, Rotation, Scale etc. is changed.
        ///     We use this to set this node as "dirty", eg that we need to update local transformations.
        /// </summary>
        public virtual void OnTransformationsSet()
        {
            _isDirty = true;
        }

        /// <summary>
        ///     Local transformations matrix, eg the result of the current local transformations.
        /// </summary>
        public Matrix localTransform = Matrix.Identity;

        /// <summary>
        ///     World transformations matrix, eg the result of the local transformations multiplied with parent transformations.
        /// </summary>
        public Matrix worldTransform = Matrix.Identity;


        /// <summary>
        ///     Calc final transformations for current frame.
        ///     This uses an indicator to know if an update is needed, so no harm is done if you call it multiple times.
        /// </summary>
        public virtual void UpdateTransformations()
        {
            // if local transformations are dirty or parent transformations are out-of-date, update world transformations
            if (_isDirty || NeedUpdateDueToParentChange())
            {
                // if we got parent, apply its transformations
                if (parentNode != null)
                {
                    // if parent need update, update it first
                    if (parentNode.transform._isDirty) parentNode.transform.UpdateTransformations();

                    // recalc world transform
                    worldTransform = localTransform * parentNode.transform.worldTransform;
                    ParentLastTransformVersion = parentNode.transform.TransformVersion;
                }
                // if not, world transformations are the same as local, and reset parent last transformations version
                else
                {
                    worldTransform = localTransform;
                    ParentLastTransformVersion = 0;
                }

                // called the function that mark world matrix change (increase transformation version etc)
                OnWorldMatrixChange();
            }

            // no longer dirty
            _isDirty = false;
        }

        /// <summary>
        ///     Return true if we need to update world transform due to parent change.
        /// </summary>
        private bool NeedUpdateDueToParentChange()
        {
            // no parent? if parent last transform version is not 0, it means we had a parent but now we don't. 
            // still require update.
            if (parentNode == null) return ParentLastTransformVersion != 0;

            // check if parent is dirty, or if our last parent transform version mismatch parent current transform version
            return parentNode.transform._isDirty || ParentLastTransformVersion != parentNode.transform.TransformVersion;
        }

        /// <summary>
        ///     Called when the world matrix of this node is actually recalculated (invoked after the calculation).
        /// </summary>
        protected virtual void OnWorldMatrixChange()
        {
            // update transformations version
            TransformVersion++;

            // trigger update event
            OnTransformationsUpdate?.Invoke(currentNode);

            // notify parent
            parentNode?.OnChildWorldMatrixChange(currentNode);
        }


        /// <summary>
        ///     Reset all local transformations.
        /// </summary>
        public void ResetTransformations()
        {
            localTransform = Matrix.Identity;
            OnTransformationsSet();
        }

        /// <summary>
        ///     Move position by vector.
        /// </summary>
        /// <param name="moveBy">Vector to translate by.</param>
        public void Translate(Vec3 moveBy)
        {
            localTransform.Translation += moveBy;
            OnTransformationsSet();
        }

    }
}
