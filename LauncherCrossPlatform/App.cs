using Infrastructure.Core.SceneGraph.Interfaces;
using NazarInfrastructure.Examples.Village.House;
using StereoKit;

namespace LauncherCrossPlatform
{
    public class App
    {
        public readonly IBase3DNode rootNode;
        public SKSettings Settings => new SKSettings
        {
            appName = "LauncherCrossPlatform",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public App()
        {
            rootNode = new HouseNode();
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