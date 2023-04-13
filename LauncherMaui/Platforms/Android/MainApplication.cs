using Android.App;
using Android.Runtime;
using System;

namespace LauncherMultiPlatform.Platforms.Android
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }
    }
}