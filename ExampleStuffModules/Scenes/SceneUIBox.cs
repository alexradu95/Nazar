using StereoKit;
using NazarInfrastructure.Scene;

namespace SKReleaseNotes
{
    class SceneUIBox : IScene
	{
		Matrix descPose    = Matrix.TRS(V.XYZ(0, 1.5f, -0.7f), Quat.LookDir(0,0,1), 2);
		string description = "A new built-in shader for indicating interactive areas! This shader is opaque, fast, and works well with Depth LSR.";
		Matrix titlePose   = Matrix.TRS(V.XYZ(0, 1.6f, -0.7f), Quat.LookDir(0, 0, 1), 4);
		string title       = "UI Box Shader";

		float    boxSize         = 0.2f;
		float    boxBorderSize   = 0.005f;
		float    boxBorderGrow   = 0.01f;
		float    boxAffectRadius = 0.2f;
		Vec3     boxHSV          = new Vec3(0,0,0.6f);
		Material boxMaterial;
		Pose     boxPose         = new Pose(V.XYZ(0,1,-0.35f), Quat.Identity);
		Pose     windowPose      = new Pose(V.XYZ(0,1.2f,-0.7f), Quat.LookDir(0,0,1));


		public void Init()
		{
			boxMaterial = Default.MaterialUIBox.Copy();
		}

		public void Shutdown()
		{
		}

		public void Step()
		{
			Hierarchy.Push(World.BoundsPose.ToMatrix());

			UI.WindowBegin("UI Box Shader Options", ref windowPose);
			UI.Label("Border Options");
			if (UI.HSlider("BorderSize", ref boxBorderSize, 0, boxSize/2, 0, 0.2f)) boxMaterial["border_size"] = boxBorderSize + boxBorderGrow - 0.01f;
			UI.SameLine();
			UI.Label($"Size {boxBorderSize*100:F1}cm");
			if (UI.HSlider("BorderGrow", ref boxBorderGrow, 0, boxSize/2, 0, 0.2f)) {
				boxMaterial["border_size"] = boxBorderSize + boxBorderGrow - 0.01f; // This is a little weird, and I might make it easier in a future update.
				boxMaterial["border_size_grow"] = boxBorderGrow;
			}
			UI.SameLine();
			UI.Label($"Grow {boxBorderGrow*100:F1}cm");
			if (UI.HSlider("AffectRadius", ref boxAffectRadius, 0, 1f, 0, 0.2f)) boxMaterial["border_affect_radius"] = boxAffectRadius;
			UI.SameLine();
			UI.Label($"Radius {boxAffectRadius*100:F1}cm");

			UI.HSeparator();
			UI.Label("Color");
			if (UI.HSlider("Hue",        ref boxHSV.v.X, 0, 1, 0, 0.2f)) boxMaterial[MatParamName.ColorTint] = Color.HSV(boxHSV).ToLinear();
			UI.SameLine(); UI.Label("Hue");
			if (UI.HSlider("Saturation", ref boxHSV.v.Y, 0, 1, 0, 0.2f)) boxMaterial[MatParamName.ColorTint] = Color.HSV(boxHSV).ToLinear();
			UI.SameLine(); UI.Label("Saturation");
			if (UI.HSlider("Value",      ref boxHSV.v.Z, 0, 1, 0, 0.2f)) boxMaterial[MatParamName.ColorTint] = Color.HSV(boxHSV).ToLinear();
			UI.SameLine(); UI.Label("Value");
			UI.WindowEnd();

			UI.Handle("InteractBox", ref boxPose, new Bounds(Vec3.One*boxSize ), false);
			Default.MeshCube.Draw(boxMaterial, boxPose.ToMatrix(boxSize));

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.6f, 0), TextFit.Wrap, TextAlign.XCenter | TextAlign.YTop, TextAlign.XCenter | TextAlign.YTop);
			Hierarchy.Pop();
		}
	}
}
