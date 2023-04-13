using Framework.HandMenu;
using Framework.SceneGraph;
using Framework.SceneGraph.Interfaces;
using Framework.Steppers.Interfaces;
using Framework.Steppers.Manager;
using Framework.Steppers.StereoKit.Framework;
using StereoKit;

namespace Nazar;

public class NazarApp
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
        //StepperManager.RegisterStepper<PassthroughFBExt>();
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