using Framework.Application.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace LauncherMultiPlatform
{
    public class App : IAppLauncher
    {
        public SKSettings Settings => new SKSettings
        {
            appName = "NazarLaboratory",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public IStepperManager StepperManager => new StepperManager();
        public ISceneGraph SceneGraphManager => new SceneGraphManager();


        public App()
        {
            StepperManager.RegisterStepper<PassthroughFBExt>();
        }

        public void Init()
        {

        }

        public void Step()
        {
            
        }
    }
}