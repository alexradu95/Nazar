using StereoKit;
using StereoKit.Framework;

namespace CoreSystem.DemoStepper
{
    internal class CubeDrawSystem : IStepper
    {


        Pose cubePose = new Pose(0, 0, -0.5f, Quat.Identity);
        Model cube;
        Matrix floorTransform = Matrix.TS(new Vec3(0, -1.5f, 0), new Vec3(30, 0.1f, 30));
        Material floorMaterial;

        public bool Enabled => true;

        public bool Initialize()
        {
            // Create assets used by the app
            cube = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                Default.MaterialUI);

            floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
            floorMaterial.Transparency = Transparency.Blend;
            return true;
        }

        public void Shutdown()
        {
            
        }

        public void Step()
        {
            if (SK.System.displayType == Display.Opaque)
                Mesh.Cube.Draw(floorMaterial, floorTransform);

            UI.Handle("Cube", ref cubePose, cube.Bounds);
            cube.Draw(cubePose.ToMatrix());
        }
    }
}
