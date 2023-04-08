using StereoKit;

namespace Framework.Application.Interfaces
{
    public interface IAppLauncher
    {
        public SKSettings Settings { get; }

        public ISceneGraph SceneGraphManager { get; }

        public IStepperManager StepperManager { get; }
    }
}