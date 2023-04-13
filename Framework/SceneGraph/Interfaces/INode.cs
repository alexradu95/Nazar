namespace Framework.SceneGraph.Interfaces;

public interface INode
{
    INode Parent { get; }
    ITransform Transform { get; }
    IChildContainer ChildContainer { get; }
    IEntityContainer EntityContainer { get; }
    bool Enabled { get; set; }
    Guid Id { get; set; }
    void Draw();
    void ForceUpdate(bool recursive = true);
}