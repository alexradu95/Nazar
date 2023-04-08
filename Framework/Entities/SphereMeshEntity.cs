using Framework.SceneGraph.Interfaces;
using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SceneGraph.CoreComponents
{
    public class SphereMeshEntity : IEntity
    {
        public bool Visible { get; set; } = true;

        public void Draw(INode parent, Matrix localTransformations, Matrix worldTransformations)
        {
            Mesh.Sphere.Draw(Material.Default, localTransformations);
        }
    }
}
