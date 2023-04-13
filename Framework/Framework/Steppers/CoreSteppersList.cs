using Framework.Steppers.Modules;
using Framework.Steppers.Modules.Passthrough;
using Framework.Steppers.StereoKit.Framework;
using StereoKit.Framework;

namespace Framework.Steppers
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
