using BaseFramework.NativeComponents.Meshes.Base;
using Infrastructure.Core.SceneGraph;
using StereoKit;

namespace NazarInfrastructure.Examples.Village.House
{
    public class HouseNode : Node3D
    {
        public override bool Initialize()
        {
            base.Initialize();

            var plane = new MeshNode()
            {
                Mesh = Mesh.GeneratePlane(new Vec2(4, 4)),
            };

            var cube = new MeshNode()
            {
                // Create a cube mesh
                Mesh = Mesh.GenerateCube(new Vec3(0.5f, 0.5f, 0.5f)),
                LocalTransform = Matrix.TRS(new Vec3(0, 1, 0), Quat.Identity, Vec3.One)
            };

            var sphere = new MeshNode()
            {
                // Create a cube mesh
                Mesh = Mesh.GenerateSphere(1),
                LocalTransform = Matrix.TRS(new Vec3(0, 2, 0), Quat.Identity, Vec3.One)
            };

            var roundedCube = new MeshNode()
            {
                // Create a cube mesh
                Mesh = Mesh.GenerateRoundedCube(new Vec3(0.5f, 0.5f, 0.5f), 0.1f),
                LocalTransform = Matrix.TRS(new Vec3(0, 2, 0), Quat.Identity, Vec3.One)
            };

            AddChild(plane);
            AddChild(cube);
            AddChild(sphere);
            AddChild(roundedCube);
            return true;
        }
    }
}
