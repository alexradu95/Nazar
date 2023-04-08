using SceneGraph.Interfaces;
using StereoKit;

namespace SceneGraph.Behaviours
{
    public class Transform : ITransform
    {
        public bool IsDirty { get; set; } = false;
        public uint TransformVersion { get; set; }
        public uint ParentLastTransformVersion { get; set; }

        private Matrix localTransform = Matrix.Identity;
        private Matrix worldTransform = Matrix.Identity;

        public Matrix LocalTransform
        {
            get => localTransform;
            set
            {
                localTransform = value;
                IsDirty = true;
            }
        }
        public Matrix WorldTransform => worldTransform;


        public void UpdateTransformations(INode parentNode)
        {
            if (IsDirty || NeedUpdateDueToParentChange(parentNode))
            {
                if (parentNode != null)
                {
                    if (parentNode.Transform.IsDirty)
                    {
                        parentNode.Transform.UpdateTransformations(parentNode);
                    }

                    worldTransform = localTransform * parentNode.Transform.WorldTransform;
                    ParentLastTransformVersion = parentNode.Transform.TransformVersion;
                }
                else
                {
                    worldTransform = localTransform;
                    ParentLastTransformVersion = 0;
                }

                OnWorldMatrixChange(parentNode);
            }

            IsDirty = false;
        }
        protected virtual void OnWorldMatrixChange(INode parentNode)
        {
            TransformVersion++;
        }
        private bool NeedUpdateDueToParentChange(INode parentNode)
        {
            if (parentNode == null)
            {
                return ParentLastTransformVersion != 0;
            }

            return parentNode.Transform.IsDirty || ParentLastTransformVersion != parentNode.Transform.TransformVersion;
        }

    }
}
