using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneGraph.Interfaces
{
    public interface IHierarchy
    {
        void AddEntity(IEntity entity);
        void RemoveEntity(IEntity entity);
        void AddChildNode(Node node);
        void RemoveChildNode(Node node);
        Node FindChildNode(string identifier, bool searchInChildren);
        void RemoveFromParent();
    }
}
