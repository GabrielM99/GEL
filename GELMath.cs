using System;
using System.Numerics;

namespace Fusyon.GEL
{
    public static class GELMath
    {
        public static bool Approximately(float a, float b)
        {
            return Math.Abs(b - a) < Math.Max(0.000001f * Math.Max(Math.Abs(a), Math.Abs(b)), float.Epsilon * 8);
        }

        public static Vector3 Normalize(Vector3 vector)
        {
            return vector == Vector3.Zero || Approximately(vector.LengthSquared(), 1f) ? vector : Vector3.Normalize(vector);
        }

        public static Vector2 Normalize(Vector2 vector)
        {
            return vector == Vector2.Zero || Approximately(vector.LengthSquared(), 1f) ? vector : Vector2.Normalize(vector);
        }

        public static Vector3 Floor(Vector3 vector)
        {
            vector.X = (float)Math.Floor(vector.X);
            vector.Y = (float)Math.Floor(vector.Y);
            vector.Z = (float)Math.Floor(vector.Z);
            return vector;
        }
    }
}