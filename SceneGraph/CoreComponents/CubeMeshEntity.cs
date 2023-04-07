using SceneGraph.Interfaces;
using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneGraph.CoreComponents
{
    public class CubeMeshEntity : IEntity
    {
        public bool Visible { get => true; set => throw new NotImplementedException(); }

        public void Draw(Node parent, Matrix localTransformations, Matrix worldTransformations)
        {
            Mesh.Cube.Draw(Material.Default, localTransformations);
        }
    }
}
