using Framework.HandMenu;
using Framework.SceneGraph;
using Framework.Steppers.Manager;
using StereoKit;

namespace Nazar;

internal class App
{
    public SKSettings Settings => new()
    {
        appName = "NazarLaboratory",
        assetsFolder = "Assets",
        displayPreference = DisplayMode.MixedReality
    };

    public StepperManager StepperManager { get; private set; }
    public SceneGraph SceneGraph { get; private set; }

    public App()
    {
        StepperManager = new StepperManager();
        SceneGraph = new SceneGraph();
    }

    /// <summary>
    /// Called before SK.Initialize is triggered.
    /// You can add here additional steppers that need to be registered before SK.Initialize.
    /// </summary>
    public void PreInit()
    {
        StepperManager.RegisterCoreSteppers();
    }

    /// <summary>
    /// Called after SK.Initialize is triggered. This is a good place to initialize your post SK.Initialize steppers.
    /// </summary>
    public void Init()
    {
        StepperManager.RegisterStepper(HandMenuManager.HandMenu);
    }

    // This Step method will be called every frame of the application
    public void Step()
    {
        SceneGraph.Draw();
    }
}