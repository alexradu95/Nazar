using Nazar;
using StereoKit;
using System;

namespace Launcher.Maui
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // This will allow the App constructor to call a few SK methods
            // before Initialize is called.
            SK.PreLoadLibrary();

            // If the app has a constructor that takes a string array, then
            // we'll use that, and pass the command line arguments into it on
            // creation
            Type appType = typeof(NazarApp);
            NazarApp app = appType.GetConstructor(new Type[] { typeof(string[]) }) != null
                ? (NazarApp)Activator.CreateInstance(appType, new object[] { args })
                : (NazarApp)Activator.CreateInstance(appType);
            if (app == null)
                throw new Exception("StereoKit loader couldn't construct an instance of the App!");
            app.PreInit();
            // Initialize StereoKit, and the app
            if (!SK.Initialize(app.Settings))
                Environment.Exit(1);
            app.Init();

            // Now loop until finished, and then shut down
            SK.Run(app.Step);
        }
    }
}