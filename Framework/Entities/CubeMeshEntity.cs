using Framework.SceneGraph.Interfaces;
using StereoKit;

namespace Framework.SceneGraph.CoreComponents;

public class CubeMeshEntity : IEntity
{
    public bool Visible { get; set; } = true;

    public void Draw(INode parent, Matrix localTransformations, Matrix worldTransformations)
    {
        Mesh.Cube.Draw(Material.Default, localTransformations);
    }
}