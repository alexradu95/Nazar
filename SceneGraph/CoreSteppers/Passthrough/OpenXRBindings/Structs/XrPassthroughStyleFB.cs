using Framework.CoreSteppers.Passthrough.OpenXRBindings.Enums;
using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Framework.CoreSteppers.Passthrough.OpenXRBindings.PassthroughOpenXRBindings;

namespace Framework.CoreSteppers.Passthrough.OpenXRBindings.Structs
{
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughStyleFB.html
    [StructLayout(LayoutKind.Sequential)]
    internal struct XrPassthroughStyleFB
    {
        public readonly XrStructureType type;
        public readonly nint next;
        public readonly float textureOpacityFactor;
        public readonly Color edgeColor;

        public XrPassthroughStyleFB(float textureOpacityFactor, Color edgeColor)
        {
            type = XrStructureType.XR_TYPE_PASSTHROUGH_STYLE_FB;
            next = nint.Zero;
            this.textureOpacityFactor = textureOpacityFactor;
            this.edgeColor = edgeColor;
        }
    }
}
