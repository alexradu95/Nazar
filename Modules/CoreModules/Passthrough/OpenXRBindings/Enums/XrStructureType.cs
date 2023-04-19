namespace Nazar.CoreModules.Passthrough.OpenXRBindings.Enums;

// https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrStructureType.html
internal enum XrStructureType : ulong
{
    XR_TYPE_PASSTHROUGH_CREATE_INFO_FB = 1000118001,
    XR_TYPE_PASSTHROUGH_LAYER_CREATE_INFO_FB = 1000118002,
    XR_TYPE_PASSTHROUGH_STYLE_FB = 1000118020,
    XR_TYPE_COMPOSITION_LAYER_PASSTHROUGH_FB = 1000118003
}