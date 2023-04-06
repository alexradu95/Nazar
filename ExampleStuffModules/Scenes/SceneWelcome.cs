using StereoKit;
using NazarInfrastructure.Scene;

namespace SKReleaseNotes
{
    class SceneWelcome : IScene
	{
		Sprite logo;
		float logoHalfWidth;

		public void Init()
		{
			logo = Sprite.FromFile("StereoKitWide.png", SpriteType.Single);
			logoHalfWidth = logo.Aspect * 0.5f * 0.25f;
		}

		public void Shutdown()
		{
		}

		public void Step()
		{
			Hierarchy.Push(World.BoundsPose.ToMatrix());
			Hierarchy.Push(Matrix.T(0, 1.4f, -0.7f));
			logo.Draw(Matrix.TRS(V.XYZ(logoHalfWidth, 0.3f, 0), Quat.LookDir(0, 0, 1), V.XYZ(logo.Aspect, 1, 1) * 0.25f));
			Text.Add("v0.3.1 Release Notes!", Matrix.TRS(V.XYZ(-logoHalfWidth, 0, 0), Quat.LookDir(0, 0, 1), 2f), V.XY(logoHalfWidth, 0), TextFit.Wrap, TextAlign.XLeft | TextAlign.YTop, TextAlign.XLeft | TextAlign.YTop);
			Hierarchy.Pop();
			Hierarchy.Pop();
		}
	}
}
