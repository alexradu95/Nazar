// This requires an addition to the Android Manifest to work on quest:
// <uses-feature android:name="com.oculus.feature.PASSTHROUGH" android:required="true" />
//
// To work on Quest+Link, you may need to enable beta features in the Oculus
// app's settings.

using Framework.Steppers.Passthrough.OpenXRBindings;
using Framework.Steppers.Passthrough.OpenXRBindings.Enums;
using Framework.Steppers.Passthrough.OpenXRBindings.Structs;
using StereoKit;
using StereoKit.Framework;

namespace Framework.Steppers.Passthrough
{
    public class PassthroughFBExt : IStepper
    {
        bool extAvailable;
        bool enabled;
        bool enabledPassthrough;
        bool enableOnInitialize;
        bool passthroughRunning;
        XrPassthroughFB activePassthrough = new XrPassthroughFB();
        XrPassthroughLayerFB activeLayer = new XrPassthroughLayerFB();
        PassthroughOpenXRBindings openXRBindings = new PassthroughOpenXRBindings();

        Color oldColor;
        bool oldSky;

        public bool Available => extAvailable;
        public bool Enabled { get => extAvailable && enabled; set => enabled = value; }
        public bool EnabledPassthrough
        {
            get => enabledPassthrough; set
            {
                if (Available && enabledPassthrough != value)
                {
                    enabledPassthrough = value;
                    if (enabledPassthrough) StartPassthrough();
                    if (!enabledPassthrough) EndPassthrough();
                }
            }
        }

        public PassthroughFBExt() : this(true) { }
        public PassthroughFBExt(bool enabled = true)
        {
            if (SK.IsInitialized)
                Log.Err("PassthroughFBExt must be constructed before StereoKit is initialized!");
            Backend.OpenXR.RequestExt("XR_FB_passthrough");
            enableOnInitialize = enabled;
        }

        public bool Initialize()
        {
            extAvailable =
                Backend.XRType == BackendXRType.OpenXR &&
                Backend.OpenXR.ExtEnabled("XR_FB_passthrough") &&
                openXRBindings.LoadOpenXRBindings();

            if (enableOnInitialize)
                EnabledPassthrough = true;
            return true;
        }

        public void Step()
        {
            if (!EnabledPassthrough) return;

            XrCompositionLayerPassthroughFB layer = new XrCompositionLayerPassthroughFB(
                XrCompositionLayerFlags.BLEND_TEXTURE_SOURCE_ALPHA_BIT, activeLayer);
            Backend.OpenXR.AddCompositionLayer(layer, -1);
        }

        public void Shutdown()
        {
            EnabledPassthrough = false;
        }

        void StartPassthrough()
        {
            if (!extAvailable) return;
            if (passthroughRunning) return;
            passthroughRunning = true;

            oldColor = Renderer.ClearColor;
            oldSky = Renderer.EnableSky;

            XrResult result = openXRBindings.xrCreatePassthroughFB(
                Backend.OpenXR.Session,
                new XrPassthroughCreateInfoFB(XrPassthroughFlagsFB.IS_RUNNING_AT_CREATION_BIT_FB),
                out activePassthrough);

            result = openXRBindings.xrCreatePassthroughLayerFB(
                Backend.OpenXR.Session,
                new XrPassthroughLayerCreateInfoFB(activePassthrough, XrPassthroughFlagsFB.IS_RUNNING_AT_CREATION_BIT_FB, XrPassthroughLayerPurposeFB.RECONSTRUCTION_FB),
                out activeLayer);

            Renderer.ClearColor = Color.BlackTransparent;
            Renderer.EnableSky = false;
        }

        void EndPassthrough()
        {
            if (!passthroughRunning) return;
            passthroughRunning = false;

            openXRBindings.xrPassthroughPauseFB(activePassthrough);
            openXRBindings.xrDestroyPassthroughLayerFB(activeLayer);
            openXRBindings.xrDestroyPassthroughFB(activePassthrough);

            Renderer.ClearColor = oldColor;
            Renderer.EnableSky = oldSky;
        }


    }
}