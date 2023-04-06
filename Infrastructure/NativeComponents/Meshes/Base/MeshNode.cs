using BaseFramework.NativeComponents.Meshes.Interface;
using Infrastructure.Core.SceneGraph;
using StereoKit;

namespace BaseFramework.NativeComponents.Meshes.Base
{
    internal class MeshNode : Node3D, IMeshNode
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

        public override void Step()
        {
            base.Step();

            Renderer.Add(mesh, Material.Default, GlobalTransform);
        }
    }
}
