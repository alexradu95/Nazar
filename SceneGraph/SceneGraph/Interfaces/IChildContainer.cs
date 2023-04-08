namespace Framework.SceneGraph.Interfaces
{
    public interface IChildContainer
    {
        List<INode> ChildNodes { get; }
        void AddChildNode(INode node);
        void RemoveChildNode(INode node);
        INode FindChildNode(Guid identifier, bool searchInChildren = true);
    }
}
