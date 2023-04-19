using System.Runtime.InteropServices;
using Nazar.CoreModules.Passthrough.OpenXRBindings.Enums;

namespace Nazar.CoreModules.Passthrough.OpenXRBindings.Structs;

// https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughLayerCreateInfoFB.html
[StructLayout(LayoutKind.Sequential)]
internal struct XrPassthroughLayerCreateInfoFb
{
    private readonly XrStructureType type;
    public readonly nint next;
    public readonly XrPassthroughFb passthrough;
    public readonly XrPassthroughFlagsFb flags;
    public readonly XrPassthroughLayerPurposeFb purpose;

    public XrPassthroughLayerCreateInfoFb(XrPassthroughFb passthrough, XrPassthroughFlagsFb flags,
        XrPassthroughLayerPurposeFb purpose)
    {
        type = XrStructureType.XR_TYPE_PASSTHROUGH_LAYER_CREATE_INFO_FB;
        next = nint.Zero;
        this.passthrough = passthrough;
        this.flags = flags;
        this.purpose = purpose;
    }
}