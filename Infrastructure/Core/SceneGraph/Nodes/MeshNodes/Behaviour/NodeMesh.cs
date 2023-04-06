using BaseFramework.Core.SceneGraph.Nodes.MeshNodes.Interfaces;
using StereoKit;

namespace BaseFramework.Core.SceneGraph.Nodes.MeshNodes.Behaviour
{
    public class NodeMesh : INodeMesh
    {
        private Mesh mesh;

        public Mesh Mesh
        {
            get { return mesh; }
            set { mesh = value; }
        }

        private Material material;

        public Material Material
        {
            get { return material; }
            set { material = value; }
        }

    }
}
