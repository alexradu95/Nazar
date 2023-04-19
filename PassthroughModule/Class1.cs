using Nazar.Framework.Interfaces;
using Nazar.PubSubHub.Hub;
using Nazar.SceneGraph;
using Nazar.StepperManager.CoreSteppers;
using StereoKit;
using StereoKit.Framework;

namespace PassthroughModule
{
    public class PassthroughModule : IAutonomousStepperModule
    {
        public string Name => "AvatarSkeleton";

        public Node RootNode => null;

        public MessagingHub InternalMessagingHub => null;

        private IStepper _stepper;
        public IStepper StereoKitStepper => _stepper;

        bool IAutonomousModule.IsEnabled { get => true; set => throw new NotImplementedException(); }

        public PassthroughModule()
        {
            _stepper = SK.AddStepper<AvatarSkeleton>();
        }

        public void Step()
        {
            
        }

        public void Shutdown()
        {
            SK.RemoveStepper<AvatarSkeleton>();
        }
    }
}