using Nazar.SceneGraph.Interfaces;
using StereoKit;

namespace Nazar.SceneGraph.Entities;

public class CubeMeshEntity : IEntity
{
    public bool Visible { get; set; } = true;

    public void Draw(INode parent, Matrix localTransformations, Matrix worldTransformations)
    {
        Mesh.Cube.Draw(Material.Default, localTransformations);
    }
}