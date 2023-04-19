using Nazar.Framework;
using Nazar.Framework.Interfaces;
using Nazar.PubSubHub.Hub;
using StereoKit;

namespace Nazar.CoreModules.Passthrough
{
    public class PassthroughModule : BaseAutonomousModule
    {
        public override string Name => "PassthroughModule";

        public override MessagingHub ExternalMessagingHub { get; }

        public PassthroughModule()
        {
            SK.AddStepper<PassthroughStepper>();
        }

        public override void Shutdown()
        {
            SK.RemoveStepper<PassthroughStepper>();
        }
    }
}