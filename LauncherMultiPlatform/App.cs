using Framework.Application.Interfaces;
using Framework.Steppers.Passthrough;
using Framework.Steppers.StereoKit.Framework;
using Nazar.Framework;
using Nazar.Framework.Stepperss;
using StereoKit;
using System;

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

        // Called before SK.Initialize is triggered
        public void PreInit()
        {
            StepperManager.RegisterStepper<PassthroughFBExt>();
            StepperManager.RegisterStepper<RenderCamera>();
        }

        // Called after SK.Initialize is triggered
        public void Init()
        {

        }

        // This Step method will be called every frame of the application
        public void Step()
        {
            
        }


    }
}