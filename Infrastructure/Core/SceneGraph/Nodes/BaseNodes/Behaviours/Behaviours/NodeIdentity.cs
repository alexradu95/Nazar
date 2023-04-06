using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces;

namespace BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Behaviours
{
    public class NodeIdentity : INodeIdentity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
    }
}
