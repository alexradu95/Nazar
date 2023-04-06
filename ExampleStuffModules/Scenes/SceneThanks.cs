using StereoKit;
using System.Diagnostics;
using NazarInfrastructure.Scene;

namespace SKReleaseNotes
{
    class SceneThanks : IScene
	{
		Matrix descPose    = Matrix.TRS(V.XYZ(0, 1.5f, -0.7f), Quat.LookDir(0,0,1), 2);
		string description = "Thanks for checking out this non-exhaustive interactive release notes demo! Check below for some links to learn more or see the full change list.";
		Matrix titlePose   = Matrix.TRS(V.XYZ(0, 1.6f, -0.7f), Quat.LookDir(0, 0, 1), 4);
		string title       = "Thank You!";

		Pose windowPose   = new Pose(V.XYZ(0,1.2f,-0.7f), Quat.LookDir(0,0,1));

		public void Init()
		{
		}

		public void Shutdown()
		{
		}

		public void Step()
		{
			Hierarchy.Push(World.BoundsPose.ToMatrix());

			UI.WindowBegin("Links", ref windowPose);

			UI.Label("Complete release notes");
			if (UI.Button("Open Notes")) OpenURL("https://github.com/maluoi/StereoKit/releases/tag/v0.3.1");

			UI.Label("StereoKit Getting Started guide");
			if (UI.Button("Open 'Getting Started'")) OpenURL("https://stereokit.net/Pages/Guides/Getting-Started.html");

			UI.Label("Join the Discord channel");
			if (UI.Button("Open Invite")) OpenURL("https://discord.gg/jtZpfS7nyK");

			UI.HSeparator();
			if (UI.Button("Exit Application")) SK.Quit();

			UI.WindowEnd();

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.6f, 0), TextFit.Wrap, TextAlign.XCenter | TextAlign.YTop, TextAlign.XCenter | TextAlign.YTop);
			Hierarchy.Pop();
		}

		void OpenURL(string url)
		{
			var psi = new ProcessStartInfo
			{
				FileName = url,
				UseShellExecute = true
			};
			Process.Start(psi);
		}
	}
}
