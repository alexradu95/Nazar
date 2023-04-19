
using Nazar.Framework.Interfaces;
using Nazar.PubSubHub.Hub;
using Nazar.SceneGraph;
using StereoKit;
using StereoKit.Framework;

namespace Nazar.CoreModules.AvatarSkeleton
{
    public class AvatarSkeletonModule : IAutonomousStepperModule
    {
        public string Name => "AvatarSkeleton";

        public Node RootNode => null;

        public MessagingHub InternalMessagingHub => null;

        private IStepper _stepper;
        public IStepper StereoKitStepper => _stepper;

        bool IAutonomousModule.IsEnabled { get => true; set => throw new NotImplementedException(); }

        public AvatarSkeletonModule()
        {
            _stepper = SK.AddStepper<AvatarSkeletonStepper>();
        }

        public void Step()
        {

        }

        public void Shutdown()
        {
            SK.RemoveStepper<AvatarSkeletonStepper>();
        }
    }
}