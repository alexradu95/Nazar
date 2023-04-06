using BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces;
using BaseFramework.Core.SceneGraph.SpatialNodes;
using StereoKit;

namespace LauncherCrossPlatform
{
    public class App
    {
        public readonly INode rootNode;
        public SKSettings Settings => new SKSettings
        {
            appName = "LauncherCrossPlatform",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public App()
        {
            rootNode = new SpatialNode();
        }

        public void Init()
        {
            rootNode.Initialize();
        }

        public void Step()
        {
            rootNode.Step();
        }
    }
}