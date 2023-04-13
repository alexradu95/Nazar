using Framework.SceneGraph.Interfaces;
using StereoKit;

namespace Framework.SceneGraph.CoreComponents;

public class SphereMeshEntity : IEntity
{
    public bool Visible { get; set; } = true;

    public void Draw(INode parent, Matrix localTransformations, Matrix worldTransformations)
    {
        Mesh.Sphere.Draw(Material.Default, localTransformations);
    }
}