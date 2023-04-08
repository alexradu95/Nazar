using SceneGraph;
using SceneGraph.Behaviours;
using SceneGraph.CoreComponents;
using StereoKit;

namespace LauncherCrossPlatform
{
    public class App
    {
        readonly Node rootNode;

        public SKSettings Settings => new SKSettings
        {
            appName = "LauncherCrossPlatform",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public App()
        {
            rootNode = new Node( null, null, null, null);
            rootNode.Entities.AddEntity(new CubeMeshEntity());

            var newNode = new Node(null, null, null, null);
            newNode.Entities.AddEntity(new SphereMeshEntity());

            rootNode.Children.AddChildNode(newNode);

            rootNode.Children.RemoveChildNode(newNode);
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