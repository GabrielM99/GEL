using System.Drawing;
using System.Numerics;

namespace Fusyon.GEL
{
    public static class GELExtensions
    {
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector4 ToVector4(this Color color)
        {
            return new Vector4(color.R, color.G, color.B, color.A);
        }

        public static Color ToColor(this Vector4 vector)
        {
            return Color.FromArgb((int)vector.X, (int)vector.Y, (int)vector.Z, (int)vector.W);
        }
    }
}