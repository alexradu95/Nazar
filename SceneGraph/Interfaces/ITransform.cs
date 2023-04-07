using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneGraph.Interfaces
{
    public interface ITransform
    {
        Matrix LocalTransformations { get; }
        Matrix WorldTransformations { get; }
        Vec3 WorldPosition { get; }
        Quat WorldRotation { get; }
        Vec3 WorldScale { get; }
    }
}
