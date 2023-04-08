using Framework.CoreSteppers.Passthrough;
using Framework.SceneGraph;
using StereoKit;
using System;

namespace LauncherCrossPlatform
{
    public class App
    {
        public static Node RootNode = new();

        public static PassthroughManager passthrough;

        public SKSettings Settings => new SKSettings
        {
            appName = "LauncherCrossPlatform",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public App()
        {
            passthrough = SK.AddStepper<PassthroughManager>();
        }

        public void Init()
        {
            RegisterSteppers();
            RegisterNodes();
        }

        private void RegisterNodes()
        {

        }

        private void RegisterSteppers()
        {
            
        }

        public void Step()
        {
            RootNode.Draw();
        }
    }
}