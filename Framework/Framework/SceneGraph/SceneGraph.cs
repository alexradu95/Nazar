using Framework.SceneGraph.Entities;
using Framework.SceneGraph.Interfaces;

namespace Framework.SceneGraph;

public class SceneGraph : ISceneGraph
{

    private Node _rootNode { get; } = new();

    public Node RootNode => _rootNode;

    public SceneGraph()
    {
        _rootNode.EntityContainer.AddEntity(new CubeMeshEntity());
    }

    public void Draw()
    {
        _rootNode.Draw();
    }

}