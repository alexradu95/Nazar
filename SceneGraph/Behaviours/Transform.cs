using SceneGraph.Interfaces;
using StereoKit;

namespace SceneGraph.Behaviours
{
    internal class Transform : ITransform
    {
        public bool IsDirty { get; set; } = false;
        public uint TransformVersion { get; set; }
        public uint ParentLastTransformVersion { get; set; }

        /// <summary>
        /// Return local transformations matrix (note: will recalculate if needed).
        /// </summary>
        public Matrix LocalTransformations => localTransform;

        /// <summary>
        /// Return world transformations matrix (note: will recalculate if needed).
        /// </summary>
        public Matrix WorldTransformations => worldTransform;


        /// <summary>
        /// Local transformations matrix, eg the result of the current local transformations.
        /// </summary>
        private Matrix localTransform = Matrix.Identity;

        /// <summary>
        /// World transformations matrix, eg the result of the local transformations multiplied with parent transformations.
        /// </summary>
        private Matrix worldTransform = Matrix.Identity;

        /// <summary>
        /// Calc final transformations for current frame.
        /// This uses an indicator to know if an update is needed, so no harm is done if you call it multiple times.
        /// </summary>
        public virtual void UpdateTransformations(INode parentNode)
        {
            // if local transformations are dirty or parent transformations are out-of-date, update world transformations
            if (IsDirty || NeedUpdateDueToParentChange(parentNode))
            {
                // if we got parent, apply its transformations
                if (parentNode != null)
                {
                    // if parent need update, update it first
                    if (parentNode.Transform.IsDirty)
                    {
                        parentNode.Transform.UpdateTransformations(parentNode);
                    }

                    // recalc world transform
                    worldTransform = localTransform * parentNode.Transform.WorldTransformations;
                    ParentLastTransformVersion = parentNode.Transform.TransformVersion;
                }
                // if not, world transformations are the same as local, and reset parent last transformations version
                else
                {
                    worldTransform = localTransform;
                    ParentLastTransformVersion = 0;
                }

                // called the function that mark world matrix change (increase transformation version etc)
                OnWorldMatrixChange(parentNode);
            }

            // no longer dirty
            IsDirty = false;
        }


        /// <summary>
        /// Called when the world matrix of this node is actually recalculated (invoked after the calculation).
        /// </summary>
        protected virtual void OnWorldMatrixChange(INode parentNode)
        {
            // update transformations version
            TransformVersion++;
        }

        /// <summary>
        /// Return true if we need to update world transform due to parent change.
        /// </summary>
        private bool NeedUpdateDueToParentChange(INode parentNode)
        {
            // no parent? if parent last transform version is not 0, it means we had a parent but now we don't. 
            // still require update.
            if (parentNode == null)
            {
                return ParentLastTransformVersion != 0;
            }

            // check if parent is dirty, or if our last parent transform version mismatch parent current transform version
            return parentNode.Transform.IsDirty || ParentLastTransformVersion != parentNode.Transform.TransformVersion;
        }

    }
}
