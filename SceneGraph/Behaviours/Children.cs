using SceneGraph.Interfaces;

namespace SceneGraph.Behaviours
{
    internal class Children : INodeChildren
    {
        /// <summary>
        /// Child nodes under this node.
        /// </summary>
        private List<INode> _childNodes = new();
        public List<INode> ChildrenList { get => _childNodes; }

        public void RemoveChildNode(Node node)
        {
            _childNodes.Remove(node);
            OnChildNodesListChange(node, false);
        }

        public Node FindChildNode(Guid identifier, bool searchInChildren = true)
        {
            foreach (Node node in _childNodes)
            {
                // search in direct children
                if (node.Id == identifier)
                {
                    return node;
                }

                // recursive search
                if (searchInChildren)
                {
                    Node foundInChild = node.Children.FindChildNode(identifier, searchInChildren);
                    if (foundInChild != null)
                    {
                        return foundInChild;
                    }
                }
            }

            return null;
        }



        virtual protected void OnChildNodesListChange(Node node, bool wasAdded)
        {
        }

    }
}
