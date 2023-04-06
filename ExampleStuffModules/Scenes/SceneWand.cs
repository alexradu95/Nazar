using StereoKit;
using System;
using NazarInfrastructure.Scene;

namespace SKReleaseNotes
{
    class SceneWand : IScene
	{
		Matrix descPose    = Matrix.TRS(V.XYZ(0, 1.5f, -0.7f), Quat.LookDir(0,0,1), 2);
		string description = "Sound sources can now be a stream of dynamic audio, whether procedural, from a mic, or the network. This wand generates a tone based on its speed, wave it around!";
		Matrix titlePose   = Matrix.TRS(V.XYZ(0, 1.6f, -0.7f), Quat.LookDir(0, 0, 1), 4);
		string title       = "Audio Streams";

		public void Init()
		{
		}

		public void Shutdown()
		{
		}

		public void Step()
		{
			Hierarchy.Push(World.BoundsPose.ToMatrix());

			StepWand();

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.6f, 0), TextFit.Wrap, TextAlign.XCenter | TextAlign.YTop, TextAlign.XCenter | TextAlign.YTop);
			Hierarchy.Pop();
		}

		Sound       wandStream;
		SoundInst   wandStreamInst;
		Pose        wandPose = new Pose(0, 1, -0.35f, Quat.Identity);
		Model       wandModel;
		Vec3        wandTipPrev;
		LinePoint[] wandFollow = null;
		float[]     wandSamples = new float[0];
		double      wandTime = 0;
		float       wandIntensity;
		void StepWand()
		{
			if (wandStream == null) { wandStream = Sound.CreateStream(5f); wandStreamInst = wandStream.Play(wandTipPrev); }
			if (wandModel  == null) wandModel = Model.FromFile("Wand.glb");
			if (wandFollow == null) { wandFollow = new LinePoint[10]; for (int i=0;i<wandFollow.Length;i+=1) wandFollow[i] = new LinePoint(Vec3.Zero, new Color(1,1,1,i/(float)wandFollow.Length), (i / (float)wandFollow.Length)*0.01f+0.001f); }

			UI.HandleBegin("wand", ref wandPose, wandModel.Bounds);
			wandModel.Draw(Matrix.Identity);
			UI.HandleEnd();

			Vec3 wandTip = wandPose.ToMatrix() * (wandModel.Bounds.center + wandModel.Bounds.dimensions.y * 0.5f * Vec3.Up);
			if (wandTipPrev.MagnitudeSq < 0.0001f) wandTipPrev = wandTip; // Prevent first frame squeak

			Vec3  wandVel   = (wandTip - wandTipPrev) * Time.Elapsedf;
			float wandSpeed = wandVel.Magnitude*100;
			
			int count = Math.Max(0, (int)(0.1f*48000) - (wandStream.TotalSamples - wandStream.CursorSamples));
			if (wandSamples.Length < count)
				wandSamples = new float[count];
			for (int i = 0; i < count; i++)
			{
				wandIntensity = MathF.Min(0.8f, SKMath.Lerp(wandIntensity, wandSpeed, 0.001f));
				wandTime += (1 / 48000.0) * (30000 * wandIntensity + 2000);
				wandSamples[i] = (float)Math.Sin(wandTime) * wandIntensity;
			}

			wandStreamInst.Position = Hierarchy.ToWorld(wandTip);
			wandStream.WriteSamples(wandSamples, count);

			for (int i = 0; i < wandFollow.Length-1; i++)
				wandFollow[i].pt = wandFollow[i+1].pt;
			wandFollow[wandFollow.Length-1].pt = wandTip;
			Lines.Add(wandFollow);
			wandTipPrev = wandTip;
		}
	}
}
