using System;

namespace Physics_Engine.Math
{
    public struct Vector2
    {
        public float x;
        public float y;

        public static Vector2 Zero = new Vector2(0, 0);
        public static Vector2 Right = new Vector2(1, 0);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Up = new Vector2(0, 1);
        public static Vector2 Down = new Vector2(0, -1);

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator *(Vector2 a, float scalar) => new Vector2(a.x * scalar, a.y * scalar);

        public static Vector2 operator /(Vector2 a, float scalar)
        {
            if (scalar == 0)
            {
                Console.Beep();
                throw new Exception("Can't Divide By Zero!");
            }

            return new Vector2(a.x / scalar, a.y / scalar);
        }

        public static Vector2 operator -(Vector2 a) => new Vector2(-1 * a.x, -1 * a.y);


        public bool Equal(Vector2 other)
        {
            if (this.x == other.x && y == other.y)
            {
                return true;
            }
            
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2 other)
            {
                return Equal(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return new { x, y }.GetHashCode();
        }

        public override string ToString()
        {
            return $"x: {x}, y: {y}";
        }
    }
}