namespace Nazar.CoreModules.Passthrough.OpenXRBindings.Enums;

// https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughFlagsFB.html
internal enum XrPassthroughFlagsFb : ulong
{
    NONE = 0,
    IS_RUNNING_AT_CREATION_BIT_FB = 0x00000001
}