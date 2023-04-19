using System.Runtime.InteropServices;
using Nazar.SKTools.OpenXRBindings.Enums;

namespace Nazar.SKTools.OpenXRBindings.Structs;

// https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughCreateInfoFB.html
[StructLayout(LayoutKind.Sequential)]
internal struct XrPassthroughCreateInfoFb
{
    private readonly XrStructureType type;
    public readonly nint next;
    public readonly XrPassthroughFlagsFb flags;

    public XrPassthroughCreateInfoFb(XrPassthroughFlagsFb passthroughFlags)
    {
        type = XrStructureType.XR_TYPE_PASSTHROUGH_CREATE_INFO_FB;
        next = nint.Zero;
        flags = passthroughFlags;
    }
}