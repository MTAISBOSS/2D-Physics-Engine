using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Physics_Engine.Graphics
{
    public class Window
    {
        private readonly GameWindow _window;
        private double theta;

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
            DrawTriangle();
            DrawRectangle();
            DrawCube();
            _window.SwapBuffers();
        }

        private void DrawCube()
        {
            GL.LoadIdentity();
            GL.Translate(0.0,0.0,-45f);
            GL.Rotate(theta,1.0,0.0,1.0);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0, 1.0, 0.0);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            GL.Color3(1.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(10.0, -10.0, 10.0);

            GL.Color3(0.0, 1.0, 1.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);

            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.Vertex3(-10.0, 10.0, -10.0);

            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.Vertex3(-10.0, 10.0, 10.0);

            
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
            GL.Rotate(theta,0,0,1);
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
        }
    }
}