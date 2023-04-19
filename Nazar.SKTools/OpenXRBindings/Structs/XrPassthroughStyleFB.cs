using System.Runtime.InteropServices;
using Nazar.SKTools.OpenXRBindings.Enums;
using StereoKit;

namespace Nazar.SKTools.OpenXRBindings.Structs;

// https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughStyleFB.html
[StructLayout(LayoutKind.Sequential)]
internal struct XrPassthroughStyleFb
{
    public readonly XrStructureType type;
    public readonly nint next;
    public readonly float textureOpacityFactor;
    public readonly Color edgeColor;

    public XrPassthroughStyleFb(float textureOpacityFactor, Color edgeColor)
    {
        type = XrStructureType.XR_TYPE_PASSTHROUGH_STYLE_FB;
        next = nint.Zero;
        this.textureOpacityFactor = textureOpacityFactor;
        this.edgeColor = edgeColor;
    }
}