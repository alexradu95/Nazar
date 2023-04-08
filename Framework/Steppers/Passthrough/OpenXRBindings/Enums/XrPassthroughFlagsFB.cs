using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Steppers.Passthrough.OpenXRBindings.Enums
{

    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughFlagsFB.html
    internal enum XrPassthroughFlagsFB : ulong
    {
        None = 0,
        IS_RUNNING_AT_CREATION_BIT_FB = 0x00000001
    }
}
