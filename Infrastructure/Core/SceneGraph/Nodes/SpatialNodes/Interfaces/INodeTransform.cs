using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFramework.Core.SceneGraph.SpatialNodes.Interfaces
{
    public interface INodeTransform
    {
        Matrix LocalTransform { get; set; }
        Matrix GlobalTransform { get; set; }
    }
}
