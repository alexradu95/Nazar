using Nazar.SceneGraph.Interfaces;

namespace Nazar.SceneGraph;

internal class ChildrenManager : IChildContainer
{
    /// <summary>
    ///     Child nodes under this node.
    /// </summary>
    public List<INode> ChildNodes { get; } = new();


    public void AddChildNode(INode node)
    {
        ChildNodes.Add(node);
        OnChildNodesListChange(node, true);
    }

    public void RemoveChildNode(INode node)
    {
        ChildNodes.Remove(node);
        OnChildNodesListChange(node, false);
    }

    public INode FindChildNode(Guid identifier, bool searchInChildren = true)
    {
        foreach (INode node in ChildNodes)
        {
            // search in direct children
            if (node.Id == identifier) return node;
            // recursive search
            if (!searchInChildren) continue;

            INode foundInChild = node.ChildContainer.FindChildNode(identifier, searchInChildren);
            if (foundInChild != null) return foundInChild;
        }

        return null;
    }

    protected virtual void OnChildNodesListChange(INode node, bool wasAdded)
    {
    }
}