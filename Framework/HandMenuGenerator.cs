using StereoKit;
using StereoKit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class HandMenuGenerator
    {

        public static HandMenuRadial BuildHandMenu()
        {
            return new HandMenuRadial(
    new HandRadialLayer("Root",
        new HandMenuItem("File", null, null, "File"),
        new HandMenuItem("Edit", null, null, "Edit"),
        new HandMenuItem("About", null, () => Log.Info(SK.VersionName)),
        new HandMenuItem("Cancel", null, null)),
    new HandRadialLayer("File",
        new HandMenuItem("New", null, () => Log.Info("New")),
        new HandMenuItem("Open", null, () => Log.Info("Open")),
        new HandMenuItem("Close", null, () => Log.Info("Close")),
        new HandMenuItem("Back", null, null, HandMenuAction.Back)),
    new HandRadialLayer("Edit",
        new HandMenuItem("Copy", null, () => Log.Info("Copy")),
        new HandMenuItem("Paste", null, () => Log.Info("Paste")),
        new HandMenuItem("Back", null, null, HandMenuAction.Back)));
        }
    }
}
