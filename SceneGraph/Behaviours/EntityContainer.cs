using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneGraph.Interfaces;

namespace SceneGraph.Behaviours
{
    internal class EntityContainer : IEntityContainer
    {

        private List<IEntity> entities = new();

        public List<IEntity> Entities => entities;

        public void AddEntity(IEntity entity)
        {
            entities.Add(entity);
            OnEntitiesListChange(entity, true);
        }

        public void RemoveEntity(IEntity entity)
        {
            entities.Remove(entity);
            OnEntitiesListChange(entity, false);
        }

        virtual protected void OnEntitiesListChange(IEntity entity, bool wasAdded)
        {
        }
    }
}
