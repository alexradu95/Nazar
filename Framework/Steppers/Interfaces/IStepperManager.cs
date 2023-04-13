using StereoKit.Framework;

namespace Framework.Steppers.Interfaces;

public interface IStepperManager
{
    void RegisterStepper<T>() where T : IStepper;

    void RegisterStepper<T>(T stepper) where T : IStepper;
}