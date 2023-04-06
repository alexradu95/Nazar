using StereoKit;

SK.Initialize("Project1");
SK.Run(() =>
{
    Mesh.Sphere.Draw(Material.Default, Matrix.TS(0, 0, -0.5f, 0.1f));
});