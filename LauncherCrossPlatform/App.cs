using StereoKit;

namespace LauncherCrossPlatform
{
    public class App
    {
        public SKSettings Settings => new SKSettings
        {
            appName = "LauncherCrossPlatform",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public VillageGraph villageGraph;

        public App()
        {

        }

        public void Init()
        {
            villageGraph = new VillageGraph("Village");
            villageGraph.Generate(10);
        }

        public void Step()
        {
            villageGraph.Draw();
        }
    }
}