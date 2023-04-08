namespace SceneGraph.Interfaces
{
    public interface INodeChildren
    {

        List<INode> ChildNodes { get; }
        void AddChildNode(INode node);
        void RemoveChildNode(INode node);
        INode FindChildNode(Guid identifier, bool searchInChildren);
    }
}
