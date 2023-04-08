using SceneGraph.Interfaces;
using StereoKit;
using System.Numerics;

namespace SceneGraph.Behaviours
{
    public class Transform : ITransform
    {
        private Matrix localTransform = Matrix.Identity;
        private Matrix worldTransform = Matrix.Identity;

        public bool IsDirty { get; set; } = false;
        public uint TransformVersion { get; set; }
        public uint ParentLastTransformVersion { get; set; }

        public Matrix LocalTransform
        {
            get => localTransform;
            set
            {
                localTransform = value;
                OnTransformationsSet();
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

        /// <summary>
        /// Get / Set node local position.
        /// </summary>
        public Vec3 Position
        {
            get { return localTransform.Translation; }
            set
            {
                if (localTransform.Translation.ToString() != value.ToString())
                {
                    localTransform.Translation = value;
                    OnTransformationsSet();
                }
            }
        }

        #region Position

        /// <summary>
        /// Alias to access position X directly.
        /// </summary>
        public float PositionX
        {
            get { return localTransform.Translation.x; }
            set
            {
                if (localTransform.Translation.x != value)
                {
                    OnTransformationsSet();
                    // How to edit Translation X
                }
            }
        }

        /// <summary>
        /// Alias to access position Y directly.
        /// </summary>
        public float PositionY
        {
            get { return localTransform.Translation.y; }
            set
            {
                if (localTransform.Translation.y != value)
                {
                    OnTransformationsSet();
                    // How to edit Translation Y
                }
            }
        }


        /// <summary>
        /// Alias to access position X directly.
        /// </summary>
        public float PositionZ
        {
            get { return localTransform.Translation.z; }
            set
            {
                if (localTransform.Translation.z != value)
                {
                    OnTransformationsSet();
                    // How to edit Translation Z
                }
            }
        }

        /// <summary>
        /// Move position by vector.
        /// </summary>
        /// <param name="moveBy">Vector to translate by.</param>
        public void Translate(Vec3 moveBy)
        {
            localTransform.Translation += moveBy;
            OnTransformationsSet();
        }

        #endregion

        #region Rotation

        /// <summary>
        /// Get / Set node local rotation.
        /// </summary>
        public Quat Rotation
        {
            get { return localTransform.Rotation; }
        }

        /// <summary>
        /// Alias to access rotation X directly.
        /// </summary>
        public float RotationX
        {
            get { return localTransform.Rotation.x; }
            set
            {
                if (localTransform.Rotation.x != value)
                {
                    // How to change the rotation of X?
                }
            }
        }

        /// <summary>
        /// Alias to access rotation Y directly.
        /// </summary>
        public float RotationY
        {
            get { return localTransform.Rotation.y; }
            set
            {
                if (localTransform.Rotation.y != value)
                {
                    // How to change the rotation of Y?
                }
            }
        }

        /// <summary>
        /// Alias to access rotation Z directly.
        /// </summary>
        public float RotationZ
        {
            get { return localTransform.Rotation.z; }
            set
            {
                if (localTransform.Rotation.z != value)
                {
                    // How to change the rotation of Z?
                }
            }
        }

        #endregion

        #region Scale

        /// <summary>
        /// Get / Set node local scale.
        /// </summary>
        public Vec3 Scale
        {
            get { return localTransform.Scale; }
            set
            {
                if (localTransform.Scale.ToString() != value.ToString())
                {
                    localTransform = localTransform.Pose.ToMatrix(value);
                    OnTransformationsSet();
                }
            }
        }

        /// <summary>
        /// Alias to access scale X directly.
        /// </summary>
        public float ScaleX
        {
            get { return localTransform.Scale.x; }
            set
            {
                if (localTransform.Scale.x != value)
                {

                    OnTransformationsSet();
                    // How to change scale on x?
                }
            }
        }


        /// <summary>
        /// Alias to access scale Y directly.
        /// </summary>
        public float ScaleY
        {
            get { return localTransform.Scale.y; }
            set
            {
                if (localTransform.Scale.y != value)
                {

                    OnTransformationsSet();
                    // How to change scale on Y?
                }
            }
        }


        /// <summary>
        /// Alias to access scale Z directly.
        /// </summary>
        public float ScaleZ
        {
            get { return localTransform.Scale.z; }
            set
            {
                if (localTransform.Scale.z != value)
                {

                    OnTransformationsSet();
                    // How to change scale on Z?
                }
            }
        }

        #endregion

        /// <summary>
        /// Called when local transformations are set, eg when Position, Rotation, Scale etc. is changed.
        /// We use this to set this node as "dirty", eg that we need to update local transformations.
        /// </summary>
        protected virtual void OnTransformationsSet()
        {
            IsDirty = true;
        }


    }
}
