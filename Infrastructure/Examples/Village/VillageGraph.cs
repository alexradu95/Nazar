using SceneGraphExample;
using StereoKit;

public class VillageGraph : Node
{
    // Create a Random instance
    private static Random random = new Random();

    public VillageGraph(string name) : base(name)
    {

    }

    public void Generate(int number)
    {
        for (int i=0; i<number; i++) {
            AddRandomNode(i);
        }
    }

    public void AddRandomNode(int i)
    {
        // Generate random float values between -0.5 and 0.5
        float randomX = (float)random.NextDouble() - 0.5f;
        float randomZ = (float)random.NextDouble() - 0.5f;


        VillageNode node = new VillageNode(Matrix.Identity, $"Village:{i}" );
        Children.Add((INode) node);
    }

    public new void Draw()
    {
        if (!Enabled) return;

        foreach (var child in Children)
        {
            child.Draw();
        }
    }


}
