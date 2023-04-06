using SceneGraphExample;
using StereoKit;

public class VillageNode : Node
{
    public Vec3 Position;
    public Mesh BuildingMesh;
    public Model BuildingModel;

    public VillageNode(Matrix position, string name) : base(position, name)
    {
        BuildingMesh = Mesh.GenerateCube(Vec3.One * 0.1f);
        BuildingModel = Model.FromMesh(BuildingMesh, Default.Material);
    }

    public void Draw()
    {
        BuildingModel.Draw(Matrix.T(Position));
    }
}
