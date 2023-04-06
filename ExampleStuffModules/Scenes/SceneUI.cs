using StereoKit;
using NazarInfrastructure.Scene;

namespace SKReleaseNotes
{
    class SceneUI : IScene
	{
		Matrix descPose    = Matrix.TRS(V.XYZ(0, 1.5f, -0.7f), Quat.LookDir(0,0,1), 2);
		string description = "A number of new UI methods, HSeparator, Text, and Push/PopTextStyle, as well as a pile of fixes and polish!";
		Matrix titlePose   = Matrix.TRS(V.XYZ(0, 1.6f, -0.7f), Quat.LookDir(0, 0, 1), 4);
		string title       = "UI Improvements";

		Pose      windowPose   = new Pose(V.XYZ(0,1.2f,-0.7f), Quat.LookDir(0,0,1));
		string    windowInput  = "text...";
		float     windowSlider = 0.2f;
		TextStyle styleHeader;

		public void Init()
		{
			Font headerFont = Font.FromFile("C:/Windows/Fonts/impact.ttf") ?? Default.Font;
			styleHeader = Text.MakeStyle(headerFont, 0.04f, Color.White);
		}

		public void Shutdown()
		{
		}

		public void Step()
		{
			Hierarchy.Push(World.BoundsPose.ToMatrix());

			UI.WindowBegin("UI Improvements", ref windowPose, V.XY(0.4f, 0));

			UI.PushTextStyle(styleHeader);
			UI.Label("Multiple fonts!");
			UI.PopTextStyle();
			UI.Text("Push or pop a TextStyle whenever you like! Make your UI a little prettier :)\n\nThe new UI.Text method allows you to add a big chunk of text to the UI, and allow it to wrap properly when it reaches the end. UI.Label will start scaling to fit instead.");
			
			UI.HSeparator();
			UI.Text("UI.Input was re-worked, and now behaves significantly better! Virtual keyboards will spawn on focus when available from the OS (currently UWP), and will accept localized keyboard input.\n\nFallback virtual keyboards and UTF-8 support are on the roadmap.");
			UI.Label("Input:"); UI.SameLine(); UI.Input("ExampleInput", ref windowInput, V.XY(UI.AreaRemaining.x, UI.LineHeight));
			
			UI.HSeparator();
			UI.Text("Far Interact behaves much nicer as well! To trigger far interact, stand out of reach of the UI, and point your hand's ray at the UI Window. Pinch when you see the ray connect.");
			
			UI.HSeparator();
			UI.HSlider("Slider", ref windowSlider, 0.1f, 0.9f, 0.1f, UI.AreaRemaining.x);
			UI.Text($"Here's an HSlider with a value of {windowSlider:F1}! A couple bugs were fixed with this UI element.");
			
			UI.WindowEnd();

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.6f, 0), TextFit.Wrap, TextAlign.XCenter | TextAlign.YTop, TextAlign.XCenter | TextAlign.YTop);
			Hierarchy.Pop();
		}
	}
}
