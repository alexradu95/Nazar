using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneGraph.Interfaces
{
    public interface IEntityContainer
    {
        /// <summary>
        /// Child entities under this node.
        /// </summary>
        List<IEntity> Entities { get; }

        void AddEntity(IEntity entity);
        void RemoveEntity(IEntity entity);
    }
}
