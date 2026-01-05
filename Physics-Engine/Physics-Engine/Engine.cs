using System;
using OpenTK;
using Physics_Engine.Graphics;

namespace Physics_Engine
{
    public class Engine
    {
        public static void Main()
        {
            GameWindow gameWindow = new GameWindow(500, 500);
            var window = new Window(gameWindow);
        }
    }
}