using BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces;
using BaseFramework.Core.SceneGraph.Nodes.MeshNodes;
using BaseFramework.Core.SceneGraph.Nodes.MeshNodes.Behaviour;
using BaseFramework.Core.SceneGraph.SpatialNodes;
using StereoKit;

namespace LauncherCrossPlatform
{
    public class App
    {
        NodeMesh mesh = new();
        public readonly INode rootNode;
        public SKSettings Settings => new SKSettings
        {
            appName = "LauncherCrossPlatform",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public App()
        {
            rootNode = new SpatialNode(null, null, null);
            rootNode.Hierarchy.AddChild(new MeshNode(null, null, null, mesh), rootNode);
        }

        public void Init()
        {
            mesh.Mesh = Mesh.GenerateSphere(1);
            mesh.Material = Material.Default;
            rootNode.Initialize();
        }

        public void Step()
        {
            rootNode.Step();
        }
    }
}