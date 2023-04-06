using StereoKit;
using System;
using System.Collections.Generic;
using System.Text;
using NazarInfrastructure.Scene;

namespace SKReleaseNotes
{
    class SceneRenderToTex : IScene
	{
		Matrix descPose    = Matrix.TRS(V.XYZ(0, 1.5f, -0.7f), Quat.LookDir(0,0,1), 2);
		string description = "Render the scene to a texture! Also comes with render layers and layer filtering :)";
		Matrix titlePose   = Matrix.TRS(V.XYZ(0, 1.6f, -0.7f), Quat.LookDir(0, 0, 1), 4);
		string title       = "Render To Texture";

		Tex      mirrorTex;
		Material mirrorMat;

		Pose mirrorPose = new Pose(V.XYZ(0, 1.1f, -0.7f), Quat.LookDir(0, 0, 1));
		Vec3 mirrorSize = V.XYZ(0.5f, 0.5f, 0.04f);

		public void Init()
		{
			mirrorTex = new Tex(TexType.Rendertarget);
			mirrorTex.SetSize(512, 512);
			mirrorTex.AddZBuffer(TexFormat.Depth16);
			mirrorMat = Default.Material.Copy();
			mirrorMat[MatParamName.DiffuseTex] = mirrorTex;
			mirrorMat.FaceCull = Cull.Front;
		}

		public void Shutdown()
		{
		}

		public void Step()
		{
			Hierarchy.Push(World.BoundsPose.ToMatrix());

			UI.HandleBegin("Mirror", ref mirrorPose, new Bounds(mirrorSize));
				Vec3  mirrorWorldPos = Hierarchy.ToWorld(Vec3.Zero);
				Vec3  mirrorWorldUp  = Hierarchy.ToWorldDirection(Vec3.Up);
				Plane plane          = new Plane(mirrorWorldPos, Hierarchy.ToWorldDirection(Vec3.Forward));
				Vec3  mirroredPos    = Input.Head.position - 2 * plane.normal * (Vec3.Dot(Input.Head.position, plane.normal)+plane.d);
				Vec3  mirroredUp     = mirrorWorldUp  - 2 * plane.normal * Vec3.Dot(mirrorWorldUp,  plane.normal);
				float distance       = Vec3.Distance(mirroredPos, mirrorWorldPos);
				float fov            = MathF.Atan(((mirrorSize.x - 0.04f) *0.5f)/distance) * 2 * Units.rad2deg;

				Renderer.RenderTo(mirrorTex, Matrix.TR(mirroredPos, Quat.LookAt(mirroredPos, mirrorWorldPos, mirroredUp)), Matrix.Perspective(fov, 1, distance, 50+distance), RenderLayer.All & ~RenderLayer.Layer1);

				Default.MeshCube.Draw(Default.Material, Matrix.S(mirrorSize), Color.White, RenderLayer.Layer1);
				Default.MeshQuad.Draw(mirrorMat, Matrix.TRS(V.XYZ(0, 0, -(mirrorSize.z/2+0.005f)), Quat.FromAngles(0,180,0), mirrorSize.x-0.04f));
			UI.HandleEnd();

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.6f, 0), TextFit.Wrap, TextAlign.XCenter | TextAlign.YTop, TextAlign.XCenter | TextAlign.YTop);
			Hierarchy.Pop();
		}
	}
}
