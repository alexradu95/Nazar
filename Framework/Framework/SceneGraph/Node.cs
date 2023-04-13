using Framework.SceneGraph.Interfaces;

namespace Framework.SceneGraph;

/// <summary>
///     A node with transformations, you can attach render able entities and scripts to it, or append
///     child nodes to inherit transformations.
/// </summary>
public class Node : INode
{
    private INode _parent;

    public Node() : this(null, null, null)
    {
    }

    public Node(IChildContainer children, ITransform transform, IEntityContainer entities)
    {
        ChildContainer = children ?? new ChildrenManager();
        Transform = transform ?? new Transform();
        EntityContainer = entities ?? new EntityContainer();
    }

    public IChildContainer ChildContainer { get; }

    public IEntityContainer EntityContainer { get; }

    public ITransform Transform { get; }


    public INode Parent
    {
        get => _parent;
        set
        {
            // Remove this node from the current parent's children list
            if (_parent != null) _parent.ChildContainer.RemoveChildNode(this);

            // Add this node to the new parent's children list
            if (value != null)
            {
                value.ChildContainer.AddChildNode(this);

                // Update the node's transformation ParentLastTransformVersion version based on the new parent's transformation
                // We will subtract 1 in order for the Nodes to be updated during the next frame
                Transform.ParentLastTransformVersion = value.Transform.TransformVersion - 1;
            }
            else
            {
                // If no new parent is provided, the node's transformation is based on its own local transformation only
                Transform.ParentLastTransformVersion = 1;
            }

            // Update the parent reference
            _parent = value;
        }
    }

    public bool Enabled { get; set; } = true;
    public Guid Id { get; set; }

    /// <summary>
    ///     Draw the node and its children.
    /// </summary>
    public virtual void Draw()
    {
        if (!Enabled) return;
        Transform.UpdateTransformations(_parent);
        // draw all child nodes
        foreach (INode node in ChildContainer.ChildNodes) node.Draw();
        // draw all child entities
        foreach (IEntity entity in EntityContainer.Entities)
            entity.Draw(this, Transform.LocalTransform, Transform.WorldTransform);
    }

    /// <summary>
    ///     Force update transformations for this node and its children.
    /// </summary>
    /// <param name="recursive">If true, will also iterate and force-update children.</param>
    public void ForceUpdate(bool recursive = true)
    {
        // not enabled? skip
        if (!Enabled) return;

        // update transformations (only if needed, testing logic is inside)
        Transform.UpdateTransformations(_parent);

        // force-update all child nodes
        if (recursive)
            foreach (Node node in ChildContainer.ChildNodes)
                node.ForceUpdate(recursive);
    }
}