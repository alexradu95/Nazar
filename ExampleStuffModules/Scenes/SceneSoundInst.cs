using StereoKit;
using System;
using NazarInfrastructure.Scene;

namespace SKReleaseNotes
{
    class SceneSoundInst : IScene
	{
		Matrix descPose    = Matrix.TRS(V.XYZ(0, 1.5f, -0.7f), Quat.LookDir(0,0,1), 2);
		string description = "Sound.Play now returns a SoundInst for reading and modifying the sound's location and state. There's also mp3 support, and improvements to the spatial audio fallback.";
		Matrix titlePose   = Matrix.TRS(V.XYZ(0, 1.6f, -0.7f), Quat.LookDir(0, 0, 1), 4);
		string title       = "Sound and Spatial Audio";

		Sound     music = Sound.FromFile("Envision.mp3");
		SoundInst musicInst;

		public void Init()
		{
		}

		public void Shutdown()
		{
			musicInst.Stop();
		}

		public void Step()
		{
			if (!musicInst.IsPlaying)
				musicInst = music.Play(Vec3.Zero);
			
			Hierarchy.Push(World.BoundsPose.ToMatrix());
			float scale = 1.5f + MathF.Sin(Time.Totalf * 3);
			Vec3 at = V.XYZ(MathF.Sin(Time.Totalf)*scale, 1, MathF.Cos(Time.Totalf)*scale);
			musicInst.Position = Hierarchy.ToWorld(at);
			
			Default.MeshSphere.Draw(Default.Material, Matrix.TS(at, 0.2f));

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.6f, 0), TextFit.Wrap, TextAlign.XCenter | TextAlign.YTop, TextAlign.XCenter | TextAlign.YTop);
			Hierarchy.Pop();
		}
	}
}
