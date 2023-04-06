using StereoKit;

namespace BaseFramework.Core.SceneGraph.Nodes.MeshNodes.Interfaces
{
    public interface INodeMesh
    {
        Mesh Mesh { get; set; }
        Material Material { get; set; }
    }
}
