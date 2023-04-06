using NazarInfrastructure.Scene;
using StereoKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKReleaseNotes
{
    class SceneLighting : IScene
	{
		Matrix descPose    = Matrix.TRS(V.XYZ(0, 1.5f, -0.7f), Quat.LookDir(0,0,1), 2);
		string description = "Brighter, less saturated, easier to get white colors in your application!";
		Matrix titlePose   = Matrix.TRS(V.XYZ(0, 1.6f, -0.7f), Quat.LookDir(0, 0, 1), 4);
		string title       = "New Default Lighting";

		Tex                defaultSkytex;
		SphericalHarmonics defaultSkylight;
		Tex                prevSkytex;
		SphericalHarmonics prevSkylight;

		Pose windowPose = new Pose(V.XYZ(0,1,-0.35f), Quat.LookDir(0,0,1));

		public SceneLighting()
		{
			defaultSkytex   = Renderer.SkyTex;
			defaultSkylight = Renderer.SkyLight;
		}

		public void Init()
		{
			prevSkytex        = Renderer.SkyTex;
			prevSkylight      = Renderer.SkyLight;
			Renderer.SkyTex   = defaultSkytex;
			Renderer.SkyLight = defaultSkylight;
		}

		public void Shutdown()
		{
			Renderer.SkyTex   = prevSkytex;
			Renderer.SkyLight = prevSkylight;
		}

		public void Step()
		{
			Hierarchy.Push(World.BoundsPose.ToMatrix());
			UI.WindowBegin("Lighting Settings", ref windowPose);
			if (UI.Button("Old Lighting"))
			{
				Renderer.SkyLight = new SphericalHarmonics(new Vec3[] {
					new Vec3( 0.27f,  0.26f,  0.25f),
					new Vec3( 0.07f,  0.09f,  0.11f),
					new Vec3(-0.06f, -0.06f, -0.04f),
					new Vec3(-0.06f, -0.04f, -0.01f),
					new Vec3(-0.04f, -0.05f, -0.06f),
					new Vec3( 0.15f,  0.16f,  0.16f),
					new Vec3(-0.04f, -0.05f, -0.05f),
					new Vec3( 0.05f,  0.05f,  0.04f),
					new Vec3(-0.11f, -0.13f, -0.13f),
				});
				Renderer.SkyTex = Tex.GenCubemap(Renderer.SkyLight);
			}
			if (UI.Button("New Lighting"))
			{
				Renderer.SkyTex   = defaultSkytex;
				Renderer.SkyLight = defaultSkylight;
			}
			UI.WindowEnd();

			Default.MeshSphere.Draw(Default.Material, Matrix.TS(V.XYZ(0, 1.1f, -0.7f), 0.4f));

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.6f, 0), TextFit.Wrap, TextAlign.XCenter | TextAlign.YTop, TextAlign.XCenter | TextAlign.YTop);
			Hierarchy.Pop();
		}
	}
}
