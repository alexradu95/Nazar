using StereoKit;

namespace SceneGraph.Interfaces
{
    public interface INode : IHierarchy, ITransform, INodeEvents
    {

        IHierarchy Hierarchy { get; }
        ITransform Transform { get;}
        INodeEvents Events { get; }

        bool Visible { get; set; }
        string Identifier { get; set; }
        object UserData { get; set; }
        Node Parent { get; }
        void Draw();
        void ForceUpdate(bool recursive = true);
        bool Empty { get; }
        bool HaveEntities { get; }
    }
}
