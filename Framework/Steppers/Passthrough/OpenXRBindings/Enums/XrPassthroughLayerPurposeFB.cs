namespace Framework.Steppers.Passthrough.OpenXRBindings.Enums;

// https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrPassthroughLayerPurposeFB.html
internal enum XrPassthroughLayerPurposeFB : uint
{
    RECONSTRUCTION_FB = 0,
    PROJECTED_FB = 1,
    TRACKED_KEYBOARD_HANDS_FB = 1000203001,
    MAX_ENUM_FB = 0x7FFFFFFF
}