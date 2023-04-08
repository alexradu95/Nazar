using StereoKit;

namespace SceneGraph.Interfaces
{
    public interface ITransform
    {
        /// <summary>
        /// The local transformation matrix of this transform object.
        /// </summary>
        Matrix LocalTransform { get; set; }

        /// <summary>
        /// The world transformation matrix of this transform object.
        /// </summary>
        Matrix WorldTransform { get; }

        /// <summary>
        /// Turns true when the transformations of this node changes.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// This number increment every time we update transformations.
        /// We use it to check if our parent's transformations had been changed since last
        /// time this node was rendered, and if so, we re-apply parent updated transformations.
        /// </summary>
        uint TransformVersion { get; }

        /// <summary>
        /// The last transformations version we got from our parent.
        /// </summary>
        uint ParentLastTransformVersion { get; }

        /// <summary>
        /// Calculates the final transformations for the current frame. This method uses an indicator to know if an 
        /// update is needed, so no harm is done if you call it multiple times. It updates the <see cref="LocalTransform"/> 
        /// and <see cref="WorldTransform"/> of this object.
        /// </summary>
        /// <param name="parentNode">The parent node of this transform object.</param>
        void UpdateTransformations(INode parentNode);

    }
}
