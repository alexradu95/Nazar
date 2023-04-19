using StereoKit.Framework;
namespace Nazar.Framework.Interfaces;

// The IAutonomousStepperModule interface extends the basic IAutonomousModule
// interface, adding the StereoKitStepper property to handle update steps
// in a StereoKit application.
public interface IAutonomousStepperModule : IAutonomousModule
{
    // The IStepper implementation for the module, which handles update steps
    // in the StereoKit application.
    public IStepper StereoKitStepper { get; }
}
