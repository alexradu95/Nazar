using System.Collections.ObjectModel;
using Framework.Steppers.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace Framework.Steppers.Manager;

public class StepperManager : IStepperManager
{
    private readonly List<IStepper> _registeredSteppers = new();

    public ReadOnlyCollection<IStepper> SteppersList => _registeredSteppers.AsReadOnly();

    public void RegisterStepper<T>() where T : IStepper
    {
        _registeredSteppers.Add(SK.AddStepper<T>());
    }

    public void RegisterStepper<T>(T stepper) where T : IStepper
    {
        _registeredSteppers.Add(SK.AddStepper(stepper));
    }
}