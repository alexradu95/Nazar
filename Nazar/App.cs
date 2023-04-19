using Nazar.CoreModules.AvatarSkeleton;
using Nazar.CoreModules.Passthrough;
using Nazar.Framework;
using StereoKit;
using StereoKit.Framework;
using System.Reflection;

namespace Nazar;

public class App : NazarApplication
{
    /// <summary>
    /// Application StereoKit settings
    /// </summary>
    public SKSettings Settings => new()
    {
        appName = "NazarLaboratory",
        assetsFolder = "Assets",
        displayPreference = DisplayMode.MixedReality
    };

    public override bool RegisterPreInitModules()
    {

        RegisterModule<PassthroughModule>();
        return true;
    }

    public override bool RegisterPostInitModules()
    {
        RegisterModule<AvatarSkeletonModule>();


        GenerateHandMenu();
        return true;
    }


}