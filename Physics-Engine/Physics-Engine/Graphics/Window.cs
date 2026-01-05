using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Physics_Engine.Graphics
{
    public class Window
    {
        private readonly GameWindow _window;
        private double theta;
        private int texture;

        public Window(GameWindow window)
        {
            _window = window;
            Start();
        }

        private void Start()
        {
            _window.Load += WindowOnLoad;
            _window.RenderFrame += WindowOnRenderFrame;
            _window.UpdateFrame += WindowOnUpdateFrame;
            _window.Resize += WindowOnResize;
            _window.Run(1f / 60);
        }

        private void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            theta += 1;
        }

        private void WindowOnResize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, _window.Width, _window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //GL.Ortho(-50, 50, -50, 50, -100, 100);
            Matrix4 mat = Matrix4.Perspective(45.0f, (float)_window.Width / _window.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref mat);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void WindowOnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.PushMatrix();
            GL.Translate(-20.0, 0.0, -45f);
            GL.Rotate(theta, 1.0, 0.0, 1.0);
            GL.Scale(0.7f, 0.7f, 0.7);
            DrawCube();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(20.0, 0.0, -45f);
            GL.Rotate(theta, -1.0, 0.0, 1.0);
            GL.Scale(0.7f, 1.5f, 1.0f);
            DrawCube();
            GL.PopMatrix();

            _window.SwapBuffers();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0, 1.0, 1.0);

            float size = 10f;

            // FRONT
            GL.Normal3(0, 0, 1);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-size, +size, +size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(+size, +size, +size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(+size, -size, +size);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-size, -size, +size);

            // LEFT
            GL.Normal3(-1, 0, 0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-size, +size, -size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-size, +size, +size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-size, -size, +size);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-size, -size, -size);

            // BACK
            GL.Normal3(0, 0, -1);
            GL.TexCoord2(0, 0);
            GL.Vertex3(+size, +size, -size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-size, +size, -size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-size, -size, -size);
            GL.TexCoord2(0, 1);
            GL.Vertex3(+size, -size, -size);

            // RIGHT
            GL.Normal3(1, 0, 0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(+size, +size, +size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(+size, +size, -size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(+size, -size, -size);
            GL.TexCoord2(0, 1);
            GL.Vertex3(+size, -size, +size);

            // TOP
            GL.Normal3(0, 1, 0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-size, +size, -size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(+size, +size, -size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(+size, +size, +size);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-size, +size, +size);

            // BOTTOM
            GL.Normal3(0, -1, 0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-size, -size, +size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(+size, -size, +size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(+size, -size, -size);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-size, -size, -size);

            GL.End();
        }

        private void DrawRectangle()
        {
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex2(0, 0);
            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex2(0, 50);
            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex2(50, 50);
            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex2(50, 0);
            GL.End();
        }

        private void DrawTriangle()
        {
            GL.LoadIdentity();
            GL.Rotate(theta, 0, 0, 1);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex2(1, 1);
            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex2(28, 1);
            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex2(16, 49);
            GL.End();
        }

        private void WindowOnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(0, 0, 0, 0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            float[] light_position = { 20, 20, 80 };
            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            float[] light_diffuse = { 1.0f, 0.5f, 0.0f };
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);

            float[] light_ambient = { 1.0f, 1.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            BitmapData image = LoadTexture(@"..\..\Resources\crate.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, image.Width, image.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, image.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        BitmapData LoadTexture(string filePath)
        {
            Bitmap bmp = new Bitmap(filePath);
            var rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData =
                bmp.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            bmp.UnlockBits(bmpData);
            return bmpData;
        }
    }
}