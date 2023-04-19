using Android.App;
using Android.Runtime;
using System;

namespace Nazar.Launcher.Maui
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