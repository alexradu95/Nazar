using StereoKit;

namespace Framework.SceneGraph.Interfaces
{
    public interface INode
    {
        INode Parent { get; }
        ITransform Transform { get; }
        IChildContainer ChildContainer { get; }
        IEntityContainer EntityContainer { get; }
        void Draw();
        void ForceUpdate(bool recursive = true);
        bool Enabled { get; set; }
        Guid Id { get; set; }
    }
}
