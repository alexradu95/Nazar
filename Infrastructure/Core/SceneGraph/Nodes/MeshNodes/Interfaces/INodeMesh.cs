using StereoKit;

namespace BaseFramework.Core.SceneGraph.Nodes.MeshNodes.Interfaces
{
    internal interface INodeMesh
    {
        Mesh Mesh { get; set; }
        Material Material { get; set; }
    }
}
