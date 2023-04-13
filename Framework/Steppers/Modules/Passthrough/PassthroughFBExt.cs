// This requires an addition to the Android Manifest to work on quest:
// <uses-feature android:name="com.oculus.feature.PASSTHROUGH" android:required="true" />
//
// To work on Quest+Link, you may need to enable beta features in the Oculus
// app's settings.

using Framework.Steppers.Modules.Passthrough.OpenXRBindings;
using Framework.Steppers.Modules.Passthrough.OpenXRBindings.Enums;
using Framework.Steppers.Modules.Passthrough.OpenXRBindings.Structs;
using StereoKit;
using StereoKit.Framework;

namespace Framework.Steppers.Modules.Passthrough;

public class PassthroughFbExt : IStepper
{
    private XrPassthroughLayerFb _activeLayer;
    private XrPassthroughFb _activePassthrough;
    private bool _enabled;
    private bool _enabledPassthrough;
    private readonly bool _enableOnInitialize;

    private Color _oldColor;
    private bool _oldSky;
    private readonly PassthroughOpenXrBindings _openXrBindings = new();
    private bool _passthroughRunning;

    public PassthroughFbExt() : this(true)
    {
    }

    public PassthroughFbExt(bool enabled = true)
    {
        if (SK.IsInitialized)
            Log.Err("PassthroughFBExt must be constructed before StereoKit is initialized!");
        Backend.OpenXR.RequestExt("XR_FB_passthrough");
        _enableOnInitialize = enabled;
    }

    public bool Available { get; private set; }

    public bool EnabledPassthrough
    {
        get => _enabledPassthrough;
        set
        {
            if (Available && _enabledPassthrough != value)
            {
                _enabledPassthrough = value;
                if (_enabledPassthrough) StartPassthrough();
                if (!_enabledPassthrough) EndPassthrough();
            }
        }
    }

    public bool Enabled
    {
        get => Available && _enabled;
        set => _enabled = value;
    }

    public bool Initialize()
    {
        Available =
            Backend.XRType == BackendXRType.OpenXR &&
            Backend.OpenXR.ExtEnabled("XR_FB_passthrough") &&
            _openXrBindings.LoadOpenXrBindings();

        if (_enableOnInitialize)
            EnabledPassthrough = true;
        return true;
    }

    public void Step()
    {
        if (!EnabledPassthrough) return;

        XrCompositionLayerPassthroughFb layer = new(
            XrCompositionLayerFlags.BLEND_TEXTURE_SOURCE_ALPHA_BIT, _activeLayer);
        Backend.OpenXR.AddCompositionLayer(layer, -1);
    }

    public void Shutdown()
    {
        EnabledPassthrough = false;
    }

    private void StartPassthrough()
    {
        if (!Available) return;
        if (_passthroughRunning) return;
        _passthroughRunning = true;

        _oldColor = Renderer.ClearColor;
        _oldSky = Renderer.EnableSky;

        XrResult result = _openXrBindings.XrCreatePassthroughFb(
            Backend.OpenXR.Session,
            new XrPassthroughCreateInfoFb(XrPassthroughFlagsFb.IS_RUNNING_AT_CREATION_BIT_FB),
            out _activePassthrough);

        result = _openXrBindings.XrCreatePassthroughLayerFb(
            Backend.OpenXR.Session,
            new XrPassthroughLayerCreateInfoFb(_activePassthrough, XrPassthroughFlagsFb.IS_RUNNING_AT_CREATION_BIT_FB,
                XrPassthroughLayerPurposeFb.RECONSTRUCTION_FB),
            out _activeLayer);

        Renderer.ClearColor = Color.BlackTransparent;
        Renderer.EnableSky = false;
    }

    private void EndPassthrough()
    {
        if (!_passthroughRunning) return;
        _passthroughRunning = false;

        _openXrBindings.XrPassthroughPauseFb(_activePassthrough);
        _openXrBindings.XrDestroyPassthroughLayerFb(_activeLayer);
        _openXrBindings.XrDestroyPassthroughFb(_activePassthrough);

        Renderer.ClearColor = _oldColor;
        Renderer.EnableSky = _oldSky;
    }
}