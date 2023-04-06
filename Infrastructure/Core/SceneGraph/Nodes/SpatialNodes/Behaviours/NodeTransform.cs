using BaseFramework.Core.SceneGraph.SpatialNodes.Interfaces;
using StereoKit;

namespace BaseFramework.Core.SceneGraph.SpatialNodes.Behaviours
{
    internal class NodeTransform : INodeTransform
    {

        private Matrix localTransform;

        public Matrix LocalTransform
        {
            get { return localTransform; }
            set { localTransform = value; }
        }

        private Matrix globalTransform;
        public Matrix GlobalTransform
        {
            get { return globalTransform; }
            set { globalTransform = value; }
        }
    }
}
