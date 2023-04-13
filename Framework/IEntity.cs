using Framework.SceneGraph.Interfaces;
using StereoKit;

namespace Framework.SceneGraph;

/// <summary>
///     A basic renderable entity.
/// </summary>
public interface IEntity
{
    /// <summary>
    ///     Return if the entity is currently visible.
    /// </summary>
    /// <returns>If the entity is visible or not.</returns>
    bool Visible { get; set; }

    /// <summary>
    ///     Draw this entity.
    /// </summary>
    /// <param name="parent">Parent node that's currently drawing this entity.</param>
    /// <param name="localTransformations">Local transformations from the direct parent node.</param>
    /// <param name="worldTransformations">
    ///     World transformations to apply on this entity (this is what you should use to draw
    ///     this entity).
    /// </param>
    void Draw(INode parent, Matrix localTransformations, Matrix worldTransformations);
}