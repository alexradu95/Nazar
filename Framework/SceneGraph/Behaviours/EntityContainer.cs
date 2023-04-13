using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.SceneGraph.Interfaces;

namespace Framework.SceneGraph.Behaviours
{
    internal class EntityContainer : IEntityContainer
    {
        public List<IEntity> Entities { get; } = new();

        public void AddEntity(IEntity entity)
        {
            Entities.Add(entity);
            OnEntitiesListChange(entity, true);
        }

        public void RemoveEntity(IEntity entity)
        {
            Entities.Remove(entity);
            OnEntitiesListChange(entity, false);
        }

        protected virtual void OnEntitiesListChange(IEntity entity, bool wasAdded)
        {
        }
    }
}
