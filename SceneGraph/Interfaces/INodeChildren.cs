using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneGraph.Interfaces
{
    public interface INodeChildren
    {
        public List<INode> ChildrenList { get; }
        void RemoveChildNode(Node node);
        Node FindChildNode(Guid identifier, bool searchInChildren);
    }
}
