using Framework;
using Framework.Application.Interfaces;
using Framework.Steppers.Passthrough;
using Framework.Steppers.StereoKit.Framework;
using StereoKit;

namespace LauncherMultiPlatform;

public class App : IAppLauncher
{
    public SKSettings Settings => new()
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
        StepperManager.RegisterStepper(HandMenuGenerator.BuildHandMenu());
    }

    // This Step method will be called every frame of the application
    public void Step()
    {
    }
}