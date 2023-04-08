using Framework.CoreSteppers.Passthrough.OpenXRBindings.Enums;
using Framework.CoreSteppers.Passthrough.OpenXRBindings.Structs;
using StereoKit;
using System.Runtime.InteropServices;

namespace Framework.CoreSteppers.Passthrough.OpenXRBindings;

// The PassthroughOpenXRBindings class provides bindings for the
// OpenXR extension which enables integration of passthrough camera functionality
internal class PassthroughOpenXRBindings
{
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrCreatePassthroughFB.html
    internal delegate XrResult del_xrCreatePassthroughFB(ulong session, [In] XrPassthroughCreateInfoFB createInfo, out XrPassthroughFB outPassthrough);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrDestroyPassthroughFB.html
    internal delegate XrResult del_xrDestroyPassthroughFB(XrPassthroughFB passthrough);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughPauseFB.html
    internal delegate XrResult del_xrPassthroughStartFB(XrPassthroughFB passthrough);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughPauseFB.html
    internal delegate XrResult del_xrPassthroughPauseFB(XrPassthroughFB passthrough);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrCreatePassthroughLayerFB.html
    internal delegate XrResult del_xrCreatePassthroughLayerFB(ulong session, [In] XrPassthroughLayerCreateInfoFB createInfo, out XrPassthroughLayerFB outLayer);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrDestroyPassthroughLayerFB.html
    internal delegate XrResult del_xrDestroyPassthroughLayerFB(XrPassthroughLayerFB layer);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughLayerPauseFB.html
    internal delegate XrResult del_xrPassthroughLayerPauseFB(XrPassthroughLayerFB layer);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughLayerResumeFB.html
    internal delegate XrResult del_xrPassthroughLayerResumeFB(XrPassthroughLayerFB layer);
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrPassthroughLayerSetStyleFB.html
    internal delegate XrResult del_xrPassthroughLayerSetStyleFB(XrPassthroughLayerFB layer, [In] XrPassthroughStyleFB style);

    // Fields for the native OpenXR function delegates.
    internal del_xrCreatePassthroughFB xrCreatePassthroughFB;
    internal del_xrDestroyPassthroughFB xrDestroyPassthroughFB;
    internal del_xrPassthroughStartFB xrPassthroughStartFB;
    internal del_xrPassthroughPauseFB xrPassthroughPauseFB;
    internal del_xrCreatePassthroughLayerFB xrCreatePassthroughLayerFB;
    internal del_xrDestroyPassthroughLayerFB xrDestroyPassthroughLayerFB;
    internal del_xrPassthroughLayerPauseFB xrPassthroughLayerPauseFB;
    internal del_xrPassthroughLayerResumeFB xrPassthroughLayerResumeFB;
    internal del_xrPassthroughLayerSetStyleFB xrPassthroughLayerSetStyleFB;

    internal bool LoadOpenXRBindings()
    {
        return
            (xrCreatePassthroughFB = Backend.OpenXR.GetFunction<del_xrCreatePassthroughFB>("xrCreatePassthroughFB")) != null &&
            (xrDestroyPassthroughFB = Backend.OpenXR.GetFunction<del_xrDestroyPassthroughFB>("xrDestroyPassthroughFB")) != null &&
            (xrPassthroughStartFB = Backend.OpenXR.GetFunction<del_xrPassthroughStartFB>("xrPassthroughStartFB")) != null &&
            (xrPassthroughPauseFB = Backend.OpenXR.GetFunction<del_xrPassthroughPauseFB>("xrPassthroughPauseFB")) != null &&
            (xrCreatePassthroughLayerFB = Backend.OpenXR.GetFunction<del_xrCreatePassthroughLayerFB>("xrCreatePassthroughLayerFB")) != null &&
            (xrDestroyPassthroughLayerFB = Backend.OpenXR.GetFunction<del_xrDestroyPassthroughLayerFB>("xrDestroyPassthroughLayerFB")) != null &&
            (xrPassthroughLayerPauseFB = Backend.OpenXR.GetFunction<del_xrPassthroughLayerPauseFB>("xrPassthroughLayerPauseFB")) != null &&
            (xrPassthroughLayerResumeFB = Backend.OpenXR.GetFunction<del_xrPassthroughLayerResumeFB>("xrPassthroughLayerResumeFB")) != null &&
            (xrPassthroughLayerSetStyleFB = Backend.OpenXR.GetFunction<del_xrPassthroughLayerSetStyleFB>("xrPassthroughLayerSetStyleFB")) != null;
    }
}
