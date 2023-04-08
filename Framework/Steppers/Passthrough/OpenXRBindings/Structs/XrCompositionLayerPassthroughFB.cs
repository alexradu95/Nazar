using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Framework.Steppers.Passthrough.OpenXRBindings.Enums;
using static Framework.Steppers.Passthrough.OpenXRBindings.PassthroughOpenXRBindings;

namespace Framework.Steppers.Passthrough.OpenXRBindings.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct XrCompositionLayerPassthroughFB
    {
        public readonly XrStructureType type;
        public readonly nint next;
        public readonly XrCompositionLayerFlags flags;
        public readonly ulong space;
        public readonly XrPassthroughLayerFB layerHandle;

        public XrCompositionLayerPassthroughFB(XrCompositionLayerFlags flags, XrPassthroughLayerFB layerHandle)
        {
            type = XrStructureType.XR_TYPE_COMPOSITION_LAYER_PASSTHROUGH_FB;
            next = nint.Zero;
            space = 0;
            this.flags = flags;
            this.layerHandle = layerHandle;
        }
    }
}
