using Framework.Application.Interfaces;
using StereoKit;
using StereoKit.Framework;
using System.Collections.ObjectModel;

namespace LauncherMultiPlatform
{
    public class StepperManager : IStepperManager
    {

        private readonly List<IStepper> registeredSteppers = new();

        public ReadOnlyCollection<IStepper> SteppersList => registeredSteppers.AsReadOnly();

        public void RegisterStepper<T>() where T : IStepper
        {
            registeredSteppers.Add(SK.AddStepper<T>());
        }

        public void RegisterStepper<T>(T stepper) where T : IStepper
        {
            registeredSteppers.Add(SK.AddStepper(stepper));
        }
    }
}