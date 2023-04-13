namespace Framework.Steppers.Modules.Passthrough.OpenXRBindings.Enums;

// https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrCompositionLayerFlags.html
internal enum XrCompositionLayerFlags : ulong
{
    NONE = 0,
    CORRECT_CHROMATIC_ABERRATION_BIT = 0x00000001,
    BLEND_TEXTURE_SOURCE_ALPHA_BIT = 0x00000002,
    UNPREMULTIPLIED_ALPHA_BIT = 0x00000004
}