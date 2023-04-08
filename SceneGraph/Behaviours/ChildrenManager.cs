using SceneGraph.Interfaces;

namespace SceneGraph
{
    internal class ChildrenManager : INodeChildren
    {
        /// <summary>
        /// Child nodes under this node.
        /// </summary>
        private readonly List<INode> childNodes = new();
        public List<INode> ChildNodes { get => childNodes; }
        public ChildrenManager() { }


        public void AddChildNode(INode node)
        {
            childNodes.Add(node);
            OnChildNodesListChange(node, true);
        }

        public void RemoveChildNode(INode node)
        {
            childNodes.Remove(node);
            OnChildNodesListChange(node, false);
        }

        public INode FindChildNode(Guid identifier, bool searchInChildren = true)
        {
            foreach (INode node in childNodes)
            {
                // search in direct children
                if (node.Id == identifier)
                {
                    return node;
                }

                // recursive search
                if (searchInChildren)
                {
                    INode foundInChild = node.Children.FindChildNode(identifier, searchInChildren);
                    if (foundInChild != null)
                    {
                        return foundInChild;
                    }
                }
            }

            return null;
        }

        virtual protected void OnChildNodesListChange(INode node, bool wasAdded)
        {
        }

    }
}
