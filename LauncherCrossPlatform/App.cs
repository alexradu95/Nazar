using SceneGraph;
using SceneGraph.CoreComponents;
using StereoKit;

namespace LauncherCrossPlatform
{
    public class App
    {

        Node rootNode;

        public SKSettings Settings => new SKSettings
        {
            appName = "LauncherCrossPlatform",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public App()
        {
            rootNode = new Node(null, null, null, null, null);
            rootNode.Entities.AddEntity(new CubeMeshEntity());
        }

        public void Init()
        {

        }

        public void Step()
        {
            rootNode.Draw();
        }
    }
}