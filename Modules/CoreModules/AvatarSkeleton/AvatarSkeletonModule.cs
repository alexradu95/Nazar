
using Nazar.CoreModules.Passthrough;
using Nazar.Framework;
using Nazar.Framework.Interfaces;
using Nazar.PubSubHub.Hub;
using Nazar.SceneGraph;
using StereoKit;
using StereoKit.Framework;

namespace Nazar.CoreModules.AvatarSkeleton
{
    public class AvatarSkeletonModule : BaseAutonomousModule
    {
        public override string Name => "AvatarSkeleton";

        public AvatarSkeletonModule()
        {
            SK.AddStepper<AvatarSkeletonStepper>();
        }

        public override void Shutdown()
        {
            SK.RemoveStepper<AvatarSkeletonStepper>();
        }
    }
}