using Infrastructure.Core.SceneGraph.Interfaces;
using StereoKit;

namespace BaseFramework.NativeComponents.Meshes.Interface
{
    public interface IMeshNode : IBase3DNode
    {
        public Mesh Mesh { get; set; }
        public Material Material { get; set; }

    }
}
