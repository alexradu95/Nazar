using StereoKit;

namespace SceneGraph
{
    public class Transform
    {
        /// <summary>
        ///     Local transformations matrix, eg the result of the current local transformations.
        /// </summary>
        public Matrix localTransform = Matrix.Identity;

        /// <summary>
        ///     World transformations matrix, eg the result of the local transformations multiplied with parent transformations.
        /// </summary>
        public Matrix worldTransform = Matrix.Identity;
    }
}
