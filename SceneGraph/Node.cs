using SceneGraph;

namespace MonoGameSceneGraph;

/// <summary>
///     A callback function you can register on different node-related events.
/// </summary>
/// <param name="node">The node instance the event came from.</param>
public delegate void NodeEventCallback(Node node);

/// <summary>
/// A node with transformations, you can attach renderable entities to it, or append child nodes to inherit
/// transformations.
/// </summary>
public class Node
{
    /// <summary>
    /// This class holds the transform properties of this node along the methods that are used to update and edit different transformation properties
    /// </summary>
    public Transform transform;

    /// <summary>
    ///     Optional identifier we can give to nodes.
    /// </summary>
    public string Identifier;

    /// <summary>
    ///     Optional user data we can attach to nodes.
    /// </summary>
    public object UserData;

    /// <summary>
    /// Is this node currently enabled?
    /// </summary>
    public virtual bool Enabled { get; set; } = true;

    public Node ParentNode
    {
        get => parentNode;
        set
        {
            parentNode = value;
            // Set the parents last transformation version to make sure we'll update world transformation next frame.
            transform.ParentLastTransformVersion = value != null ? value.transform.TransformVersion - 1 : 1;
        }
    }

    private List<IEntity> childEntities = new();
    private List<Node> childNodes = new();
    private Node parentNode;

    public Node()
    {
        transform = new Transform(this);
    }


    /// <summary>
    ///     Draw the node and its children.
    /// </summary>
    public virtual void Draw()
    {
        if (!Enabled) return;
        transform.UpdateTransformations();
        foreach (Node node in childNodes) node.Draw();
        foreach (IEntity entity in childEntities) entity.Draw(this, transform.localTransform, transform.worldTransform);
    }

    #region Hierarchy Management

    /// <summary>
    ///     Add an entity to this node.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    public void AddEntity(IEntity entity)
    {
        childEntities.Add(entity);
        OnEntitiesListChange(entity, true);
    }

    /// <summary>
    ///     Remove an entity from this node.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    public void RemoveEntity(IEntity entity)
    {
        childEntities.Remove(entity);
        OnEntitiesListChange(entity, false);
    }

    /// <summary>
    ///     Called whenever a child node was added / removed from this node.
    /// </summary>
    /// <param name="entity">Entity that was added / removed.</param>
    /// <param name="wasAdded">If true its an entity that was added, if false, an entity that was removed.</param>
    protected virtual void OnEntitiesListChange(IEntity entity, bool wasAdded)
    {
    }

    /// <summary>
    ///     Called whenever an entity was added / removed from this node.
    /// </summary>
    /// <param name="node">Node that was added / removed.</param>
    /// <param name="wasAdded">If true its a node that was added, if false, a node that was removed.</param>
    protected virtual void OnChildNodesListChange(Node node, bool wasAdded)
    {
    }

    /// <summary>
    ///     Add a child node to this node.
    /// </summary>
    /// <param name="node">Node to add.</param>
    public void AddChildNode(Node node)
    {
        // node already got a parent?
        if (node.ParentNode != null) throw new Exception("Can't add a node that already have a parent.");

        // add node to children list
        childNodes.Add(node);

        // set self as node's parent
        node.SetParent(this);
        OnChildNodesListChange(node, true);
    }

    /// <summary>
    ///     Remove a child node from this node.
    /// </summary>
    /// <param name="node">Node to add.</param>
    public void RemoveChildNode(Node node)
    {
        // make sure the node is a child of this node
        if (node.ParentNode != this) throw new Exception("Can't remove a node that don't belong to this parent.");

        // remove node from children list
        childNodes.Remove(node);

        // clear node parent
        node.SetParent(null);
        OnChildNodesListChange(node, false);
    }

    /// <summary>
    ///     Find and return first child node by identifier.
    /// </summary>
    /// <param name="identifier">Node identifier to search for.</param>
    /// <param name="searchInChildren">If true, will also search recursively in children.</param>
    /// <returns>Node with given identifier or null if not found.</returns>
    public Node FindChildNode(string identifier, bool searchInChildren = true)
    {
        foreach (Node node in childNodes)
        {
            // search in direct children
            if (node.Identifier == identifier) return node;

            // recursive search
            if (searchInChildren)
            {
                Node foundInChild = node.FindChildNode(identifier, searchInChildren);
                if (foundInChild != null) return foundInChild;
            }
        }

        // if got here it means we didn't find any child node with given identifier
        return null;
    }

    /// <summary>
    ///     Remove this node from its parent.
    /// </summary>
    public void RemoveFromParent()
    {
        // don't have a parent?
        if (parentNode == null) throw new Exception("Can't remove an orphan node from parent.");

        // remove from parent
        parentNode.RemoveChildNode(this);
    }



    /// <summary>
    ///     Set the parent of this node.
    /// </summary>
    /// <param name="newParent">New parent node to set, or null for no parent.</param>
    protected virtual void SetParent(Node newParent)
    {
    }

    #endregion


    /// <summary>
    ///     Force update transformations for this node and its children.
    /// </summary>
    /// <param name="recursive">If true, will also iterate and force-update children.</param>
    public void ForceUpdate(bool recursive = true)
    {
        // not visible? skip
        if (!Enabled) return;

        // update transformations (only if needed, testing logic is inside)
        transform.UpdateTransformations();

        // force-update all child nodes
        if (recursive)
            foreach (Node node in childNodes)
                node.ForceUpdate(recursive);
    }

    /// <summary>
    ///     Called every time one of the child nodes recalculate world transformations.
    /// </summary>
    /// <param name="node">The child node that updated.</param>
    public virtual void OnChildWorldMatrixChange(Node node)
    {
    }

    #region Utility Methods


    /// <summary>
    ///     Clone this scene node.
    /// </summary>
    /// <returns>Node copy.</returns>
    public virtual Node Clone()
    {
        Node ret = new();
        ret.transform.localTransform = transform.localTransform;
        ret.Enabled = Enabled;
        return ret;
    }

    #endregion
}