using Framework.Steppers.Passthrough.OpenXRBindings.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Framework.Steppers.Passthrough.OpenXRBindings.PassthroughOpenXRBindings;

namespace Framework.Steppers.Passthrough.OpenXRBindings.Structs
{
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughLayerCreateInfoFB.html
    [StructLayout(LayoutKind.Sequential)]
    internal struct XrPassthroughLayerCreateInfoFB
    {
        private readonly XrStructureType type;
        public readonly nint next;
        public readonly XrPassthroughFB passthrough;
        public readonly XrPassthroughFlagsFB flags;
        public readonly XrPassthroughLayerPurposeFB purpose;

        public XrPassthroughLayerCreateInfoFB(XrPassthroughFB passthrough, XrPassthroughFlagsFB flags,
            XrPassthroughLayerPurposeFB purpose)
        {
            type = XrStructureType.XR_TYPE_PASSTHROUGH_LAYER_CREATE_INFO_FB;
            next = nint.Zero;
            this.passthrough = passthrough;
            this.flags = flags;
            this.purpose = purpose;
        }
    }
}
