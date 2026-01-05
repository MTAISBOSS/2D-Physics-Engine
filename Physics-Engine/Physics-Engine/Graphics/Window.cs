using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ClearBufferMask = OpenTK.Graphics.OpenGL4.ClearBufferMask;
using GL = OpenTK.Graphics.OpenGL4.GL;

namespace Physics_Engine.Graphics
{
    public class Window
    {
        private readonly GameWindow _window;
        public Window(GameWindow window)
        {
            _window = window;
            Start();
        }

        private void Start()
        {
            _window.Load += WindowOnLoad;
            _window.RenderFrame += WindowOnRenderFrame;
            _window.Run(1f / 60);
        }

        private void WindowOnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _window.SwapBuffers();
        }

        private void WindowOnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(0,0,0,0);
        }
    }
}