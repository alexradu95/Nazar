using BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces;
using BaseFramework.Core.SceneGraph.Nodes.MeshNodes.Interfaces;
using BaseFramework.Core.SceneGraph.SpatialNodes;
using BaseFramework.Core.SceneGraph.SpatialNodes.Interfaces;
using StereoKit;

namespace BaseFramework.Core.SceneGraph.Nodes.MeshNodes
{
    public class MeshNode : SpatialNode
    {
        INodeMesh mesh;

        public MeshNode(INodeIdentity identity, INodeHierarchy hierarchy, INodeTransform transform, INodeMesh mesh) : base(identity, hierarchy, transform)
        {
            this.mesh = mesh;
        }

        public override void Step()
        {
            base.Step();
            DrawMesh();
        }

        public void DrawMesh()
        {
            if (mesh.Mesh != null && mesh.Material != null)
            {
                Mesh.Cube.Draw(mesh.Material, base.Transform.GlobalTransform);

            }
        }
    }
}
