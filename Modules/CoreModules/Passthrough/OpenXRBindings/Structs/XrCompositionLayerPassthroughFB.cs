﻿using System.Runtime.InteropServices;
using Nazar.CoreModules.Passthrough.OpenXRBindings.Enums;

namespace Nazar.CoreModules.Passthrough.OpenXRBindings.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct XrCompositionLayerPassthroughFb
{
    public readonly XrStructureType type;
    public readonly nint next;
    public readonly XrCompositionLayerFlags flags;
    public readonly ulong space;
    public readonly XrPassthroughLayerFb layerHandle;

    public XrCompositionLayerPassthroughFb(XrCompositionLayerFlags flags, XrPassthroughLayerFb layerHandle)
    {
        type = XrStructureType.XR_TYPE_COMPOSITION_LAYER_PASSTHROUGH_FB;
        next = nint.Zero;
        space = 0;
        this.flags = flags;
        this.layerHandle = layerHandle;
    }
}