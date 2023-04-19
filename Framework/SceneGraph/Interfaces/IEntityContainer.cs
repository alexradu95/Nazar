namespace Nazar.SceneGraph.Interfaces;

public interface IEntityContainer
{
    /// <summary>
    ///     Child entities under this node.
    /// </summary>
    List<IEntity> Entities { get; }

    void AddEntity(IEntity entity);
    void RemoveEntity(IEntity entity);
}