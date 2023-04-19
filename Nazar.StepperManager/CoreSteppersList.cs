using Framework.Steppers.StereoKit.Framework;
using Nazar.StepperManager.CoreSteppers;

namespace Nazar.StepperManager
{
    internal static class CoreSteppersList
    {
        public readonly static HashSet<Type> Steppers = new()
        {
            typeof(AvatarSkeleton),
            typeof(RenderCamera),
            typeof(PassthroughFbExt),
        };
    }
}
