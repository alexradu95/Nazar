using System.Runtime.InteropServices;
using Framework.Steppers.Modules.Passthrough.OpenXRBindings.Enums;
using Framework.Steppers.Modules.Passthrough.OpenXRBindings.Structs;
using StereoKit;

namespace Framework.Steppers.Modules.Passthrough.OpenXRBindings;

// The PassthroughOpenXRBindings class provides bindings for the
// OpenXR extension which enables integration of passthrough camera functionality
internal class PassthroughOpenXrBindings
{
    // Fields for the native OpenXR function delegates.
    internal DelXrCreatePassthroughFb XrCreatePassthroughFb;
    internal DelXrCreatePassthroughLayerFb XrCreatePassthroughLayerFb;
    internal DelXrDestroyPassthroughFb XrDestroyPassthroughFb;
    internal DelXrDestroyPassthroughLayerFb XrDestroyPassthroughLayerFb;
    internal DelXrPassthroughLayerPauseFb XrPassthroughLayerPauseFb;
    internal DelXrPassthroughLayerResumeFb XrPassthroughLayerResumeFb;
    internal DelXrPassthroughLayerSetStyleFb XrPassthroughLayerSetStyleFb;
    internal DelXrPassthroughPauseFb XrPassthroughPauseFb;
    internal DelXrPassthroughStartFb XrPassthroughStartFb;

    internal bool LoadOpenXrBindings()
    {
        return
            (XrCreatePassthroughFb = Backend.OpenXR.GetFunction<DelXrCreatePassthroughFb>("xrCreatePassthroughFB")) !=
            null &&
            (XrDestroyPassthroughFb =
                Backend.OpenXR.GetFunction<DelXrDestroyPassthroughFb>("xrDestroyPassthroughFB")) != null &&
            (XrPassthroughStartFb = Backend.OpenXR.GetFunction<DelXrPassthroughStartFb>("xrPassthroughStartFB")) !=
            null &&
            (XrPassthroughPauseFb = Backend.OpenXR.GetFunction<DelXrPassthroughPauseFb>("xrPassthroughPauseFB")) !=
            null &&
            (XrCreatePassthroughLayerFb =
                Backend.OpenXR.GetFunction<DelXrCreatePassthroughLayerFb>("xrCreatePassthroughLayerFB")) != null &&
            (XrDestroyPassthroughLayerFb =
                Backend.OpenXR.GetFunction<DelXrDestroyPassthroughLayerFb>("xrDestroyPassthroughLayerFB")) != null &&
            (XrPassthroughLayerPauseFb =
                Backend.OpenXR.GetFunction<DelXrPassthroughLayerPauseFb>("xrPassthroughLayerPauseFB")) != null &&
            (XrPassthroughLayerResumeFb =
                Backend.OpenXR.GetFunction<DelXrPassthroughLayerResumeFb>("xrPassthroughLayerResumeFB")) != null &&
            (XrPassthroughLayerSetStyleFb =
                Backend.OpenXR.GetFunction<DelXrPassthroughLayerSetStyleFb>("xrPassthroughLayerSetStyleFB")) != null;
    }

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrCreatePassthroughFB.html
    internal delegate XrResult DelXrCreatePassthroughFb(ulong session, [In] XrPassthroughCreateInfoFb createInfo,
        out XrPassthroughFb outPassthrough);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrDestroyPassthroughFB.html
    internal delegate XrResult DelXrDestroyPassthroughFb(XrPassthroughFb passthrough);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughPauseFB.html
    internal delegate XrResult DelXrPassthroughStartFb(XrPassthroughFb passthrough);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughPauseFB.html
    internal delegate XrResult DelXrPassthroughPauseFb(XrPassthroughFb passthrough);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrCreatePassthroughLayerFB.html
    internal delegate XrResult DelXrCreatePassthroughLayerFb(ulong session,
        [In] XrPassthroughLayerCreateInfoFb createInfo, out XrPassthroughLayerFb outLayer);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrDestroyPassthroughLayerFB.html
    internal delegate XrResult DelXrDestroyPassthroughLayerFb(XrPassthroughLayerFb layer);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughLayerPauseFB.html
    internal delegate XrResult DelXrPassthroughLayerPauseFb(XrPassthroughLayerFb layer);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughLayerResumeFB.html
    internal delegate XrResult DelXrPassthroughLayerResumeFb(XrPassthroughLayerFb layer);

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughLayerSetStyleFB.html
    internal delegate XrResult DelXrPassthroughLayerSetStyleFb(XrPassthroughLayerFb layer,
        [In] XrPassthroughStyleFb style);
}