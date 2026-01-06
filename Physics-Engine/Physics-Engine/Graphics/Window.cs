using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Physics_Engine.Graphics
{
    public class Window
    {
        private readonly GameWindow _window;
        private double theta;
        private int texture;
        private double xOffset = 0;
        private double yOffset = 0;
        private double moveThreshold = 1f;
        private double zOffset = 0;

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
            _window.KeyPress += WindowOnKeyPress;
            _window.KeyDown += WindowOnKeyDown;
            _window.KeyUp += WindowOnKeyUp;
            _window.Run(1f / 60);
        }

        private void WindowOnKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            Console.WriteLine($"Key up: {e.Key}");
        }

        private void WindowOnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            Console.WriteLine($"Key down: {e.Key}");
            /*
             switch (e.Key)
             {
                 case Key.A:
                     xOffset -= moveThreshold;
                     break;
                 case Key.D:
                     xOffset += moveThreshold;
                     break;
             }
             switch (e.Key)
             {
                 case Key.S:
                     yOffset -= moveThreshold;
                     break;
                 case Key.W:
                     yOffset += moveThreshold;
                     break;
             }*/
        }

        private void WindowOnKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine($"Key pressed: {e.KeyChar}");
        }

        private void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            theta += 1;
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Key.A))
            {
                xOffset -= moveThreshold;
            }

            if (keyboardState.IsKeyDown(Key.D))
            {
                xOffset += moveThreshold;
            }

            if (keyboardState.IsKeyDown(Key.W))
            {
                yOffset += moveThreshold;
            }

            if (keyboardState.IsKeyDown(Key.S))
            {
                yOffset -= moveThreshold;
            }

            if (keyboardState.IsKeyDown(Key.E))
            {
                zOffset += moveThreshold;
            }

            if (keyboardState.IsKeyDown(Key.Q))
            {
                zOffset -= moveThreshold;
            }
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
            GL.Translate(-20.0 + xOffset, 0.0 + yOffset, -45f);
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

            GL.PushMatrix();
            GL.Translate(0.0, 0.0, -25f);
            DrawQuad();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f + xOffset, 0.0f + yOffset, -25f + zOffset); 
            GL.Rotate(theta * 0.5f, 1, 1, 1);
            DrawTexturedSphere(5.0f, 32, 16, texture);
            GL.PopMatrix();
            _window.SwapBuffers();
        }

        private void DrawQuad()
        {
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Lighting);
            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
            GL.Begin(PrimitiveType.Quads);

            GL.Color4(0.0, 0.0, 1.0, 0.6);
            GL.Vertex3(0.0, 0.0, 0.0);
            GL.Vertex3(5.0, 0.0, 0.0);
            GL.Vertex3(5.0, 5.0, 0.0);
            GL.Vertex3(0.0, 5.0, 0.0);

            GL.End();
            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Texture2D);
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

        private void DrawTexturedSphere(float radius, int slices, int stacks, int textureId)
        {
            GL.Enable(EnableCap.ColorMaterial);
            GL.Color3(1.0f, 1.0f, 1.0f);

            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < stacks; i++)
            {
                float phi0 = (float)(System.Math.PI * i / stacks);
                float phi1 = (float)(System.Math.PI * (i + 1) / stacks);

                for (int j = 0; j < slices; j++)
                {
                    float theta0 = (float)(2 * System.Math.PI * j / slices);
                    float theta1 = (float)(2 * System.Math.PI * (j + 1) / slices);

                    float u0 = j / (float)slices;
                    float u1 = (j + 1) / (float)slices;
                    float v0 = i / (float)stacks;
                    float v1 = (i + 1) / (float)stacks;

                    var v00 = GetSpherePoint(radius, phi0, theta0);
                    var v01 = GetSpherePoint(radius, phi0, theta1);
                    var v11 = GetSpherePoint(radius, phi1, theta1);
                    var v10 = GetSpherePoint(radius, phi1, theta0);

                    var n00 = Vector3.Normalize(v00);
                    var n01 = Vector3.Normalize(v01);
                    var n11 = Vector3.Normalize(v11);
                    var n10 = Vector3.Normalize(v10);

                    GL.TexCoord2(u0, v0);
                    GL.Normal3(n00);
                    GL.Vertex3(v00);

                    GL.TexCoord2(u1, v0);
                    GL.Normal3(n01);
                    GL.Vertex3(v01);

                    GL.TexCoord2(u1, v1);
                    GL.Normal3(n11);
                    GL.Vertex3(v11);

                    GL.TexCoord2(u0, v1);
                    GL.Normal3(n10);
                    GL.Vertex3(v10);
                }
            }

            GL.End();
        }

        private Vector3 GetSpherePoint(float radius, float phi, float theta)
        {
            float x = (float)(radius * System.Math.Sin(phi) * System.Math.Cos(theta));
            float y = (float)(radius * System.Math.Cos(phi));
            float z = (float)(radius * System.Math.Sin(phi) * System.Math.Sin(theta));
            return new Vector3(x, y, z);
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
            float[] lightPosition = { 20f, 20f, 80f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            float[] lightDiffuse = { 1.0f, 1.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);

            float[] lightAmbient = { 1.0f, 1.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.ColorMaterial);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            BitmapData image = LoadTexture(@"..\..\Resources\crate.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, image.Width, image.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, image.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);


            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
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