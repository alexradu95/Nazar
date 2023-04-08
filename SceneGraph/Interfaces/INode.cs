using StereoKit;

namespace SceneGraph.Interfaces
{
    public interface INode
    {

        INodeChildren Children { get; }
        ITransform Transform { get;}
        INodeEvents Events { get; }

        bool Enabled { get; set; }
        Guid Id { get; set; }
        INode Parent { get; }
        void Draw();
        void ForceUpdate(bool recursive = true);
    }
}
