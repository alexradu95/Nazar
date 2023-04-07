using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneGraph.Interfaces
{
    public interface INodeEvents
    {
        event NodeEventCallback OnTransformationsUpdate;
        event NodeEventCallback OnDraw;
    }
}
