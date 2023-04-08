using StereoKit.Framework;

namespace Framework.Application.Interfaces
{
    public interface IStepperManager
    {
        void RegisterStepper<T>() where T : IStepper; 
    }
}