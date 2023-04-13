using StereoKit;
using StereoKit.Framework;

namespace Framework.Steppers
{
    namespace StereoKit.Framework
    {
        public class RenderCamera : IStepper
        {
            private int _frameIndex;
            private Material _frameMaterial;
            private Tex _frameSurface;
            private float _frameTime;
            private bool _previewing;
            private bool _recording;
            private Pose _renderFrom;
            public Pose at;

            private Color32[] buffer;
            public float damping = 8;

            public string folder = "Video";
            public Pose from;

            public RenderCamera()
            {
                Width = 500;
                Height = 500;
                FrameRate = 12;

                at = Pose.Identity;
                from = new Pose(at.position + V.XYZ(0, 0, 0.1f) * at.orientation, at.orientation);
                _renderFrom = at;
            }

            public int Width { get; }
            public int Height { get; }
            public int FrameRate { get; }
            public bool Enabled => true;

            public bool Initialize()
            {
                _frameSurface = new Tex(TexType.Rendertarget);
                _frameSurface.SetSize(Width, Height);
                _frameSurface.AddZBuffer(TexFormat.Depth32);
                _frameMaterial = Default.MaterialUnlit.Copy();
                _frameMaterial[MatParamName.DiffuseTex] = _frameSurface;
                _frameMaterial.FaceCull = Cull.None;
                return true;
            }

            public void Shutdown()
            {
            }

            public void Step()
            {
                UI.PushId("RenderCameraWidget");
                UI.Handle("from", ref from, new Bounds(Vec3.One * 0.02f), true);
                UI.HandleBegin("at", ref at, new Bounds(Vec3.One * 0.02f), true);
                UI.ToggleAt("On", ref _previewing, new Vec3(4, -2, 0) * U.cm, new Vec2(8 * U.cm, UI.LineHeight));
                if (_previewing && UI.ToggleAt("Record", ref _recording, new Vec3(4, -6, 0) * U.cm,
                        new Vec2(8 * U.cm, UI.LineHeight)))
                {
                    _frameTime = Time.StepUnscaledf;
                    _frameIndex = 0;
                }

                UI.HandleEnd();
                UI.PopId();

                float fov = 10 + Math.Max(0, Math.Min(1, (Vec3.Distance(from.position, at.position) - 0.1f) / 0.2f)) *
                    110;
                Vec3 previewAt = at.position + at.orientation * Vec3.Up * 0.06f;
                Vec3 renderFrom = at.position + (at.position - from.position).Normalized * 0.06f;
                _renderFrom = Pose.Lerp(_renderFrom, new Pose(renderFrom, Quat.LookDir(at.position - from.position)),
                    Time.Stepf * damping);

                Lines.Add(from.position, at.position, Color.White, 0.005f);
                from.orientation = at.orientation = Quat.LookDir(from.position - at.position);


                if (_previewing)
                {
                    Hierarchy.Push(Matrix.TR(previewAt, Quat.LookAt(previewAt, Input.Head.position)));
                    Default.MeshQuad.Draw(_frameMaterial, Matrix.S(V.XYZ(0.08f * ((float) Width / Height), 0.08f, 1)));
                    Text.Add("" + (int) fov, Matrix.TS(-0.03f, 0, 0, 0.5f), TextAlign.CenterLeft);
                    Hierarchy.Pop();

                    Renderer.RenderTo(_frameSurface,
                        _renderFrom.ToMatrix(),
                        Matrix.Perspective(fov, (float) Width / Height, 0.01f, 100));
                }

                if (_recording)
                    SaveFrame(FrameRate);
            }

            private void SaveFrame(int framerate)
            {
                float rateTime = 1.0f / framerate;
                if (_frameTime + rateTime < Time.TotalUnscaledf)
                {
                    _frameTime = Time.TotalUnscaledf;
                    _frameSurface.GetColors(ref buffer);

                    Directory.CreateDirectory(folder);
                    Stream writer = new FileStream($"{folder}/image{_frameIndex:D4}.bmp", FileMode.Create);
                    WriteBitmap(writer, _frameSurface.Width, _frameSurface.Height, buffer);
                    writer.Close();
                    _frameIndex += 1;
                }
            }

            private static void WriteBitmap(Stream stream, int width, int height, Color32[] imageData)
            {
                using (BinaryWriter bw = new(stream))
                {
                    // define the bitmap file header
                    bw.Write((ushort) 0x4D42); // bfType;
                    bw.Write((uint) (14 + 40 + width * height * 4)); // bfSize;
                    bw.Write((ushort) 0); // bfReserved1;
                    bw.Write((ushort) 0); // bfReserved2;
                    bw.Write((uint) 14 + 40); // bfOffBits;

                    // define the bitmap information header
                    bw.Write((uint) 40); // biSize;
                    bw.Write(width); // biWidth;
                    bw.Write(height); // biHeight;
                    bw.Write((ushort) 1); // biPlanes;
                    bw.Write((ushort) 32); // biBitCount;
                    bw.Write((uint) 0); // biCompression;
                    bw.Write((uint) (width * height * 4)); // biSizeImage;
                    bw.Write(0); // biXPelsPerMeter;
                    bw.Write(0); // biYPelsPerMeter;
                    bw.Write((uint) 0); // biClrUsed;
                    bw.Write((uint) 0); // biClrImportant;

                    // switch the image data from RGB to BGR
                    for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        int i = x + (height - 1 - y) * width;
                        bw.Write(imageData[i].b);
                        bw.Write(imageData[i].g);
                        bw.Write(imageData[i].r);
                        bw.Write(imageData[i].a);
                    }
                }
            }
        }
    }
}