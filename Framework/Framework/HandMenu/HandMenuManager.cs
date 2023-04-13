using StereoKit;
using StereoKit.Framework;

namespace Framework.HandMenu;

public static class HandMenuManager
{
    public static HandMenuRadial HandMenu = new(
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
