using UnityEngine;

namespace Fusyon.GEL.Unity
{
	internal static class GELUnityExtensions
	{
		public static System.Numerics.Vector2 ToGEL(this Vector2 vector)
		{
			return new System.Numerics.Vector2(vector.x, vector.y);
		}

		public static Vector2 ToUnity(this System.Numerics.Vector2 vector)
		{
			return new Vector2(vector.X, vector.Y);
		}

		public static System.Numerics.Vector3 ToGEL(this Vector3 vector)
		{
			return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
		}

		public static Vector3 ToUnity(this System.Numerics.Vector3 vector)
		{
			return new Vector3(vector.X, vector.Y, vector.Z);
		}

		public static System.Numerics.Matrix4x4 ToGEL(this Matrix4x4 matrix)
		{
			return new System.Numerics.Matrix4x4(matrix.m00, matrix.m10, matrix.m20, matrix.m30, matrix.m01, matrix.m11, matrix.m21, matrix.m31, matrix.m02, matrix.m12, matrix.m22, matrix.m32, matrix.m03, matrix.m13, matrix.m23, matrix.m33);
		}

		public static Matrix4x4 ToUnity(this System.Numerics.Matrix4x4 matrix)
		{
			return new Matrix4x4(new Vector4(matrix.M11, matrix.M21, matrix.M31, matrix.M41), new Vector4(matrix.M12, matrix.M22, matrix.M32, matrix.M42), new Vector4(matrix.M13, matrix.M23, matrix.M33, matrix.M43), new Vector4(matrix.M12, matrix.M24, matrix.M34, matrix.M44));
		}

		public static System.Drawing.Color ToGEL(this Color color)
		{
			return System.Drawing.Color.FromArgb((int)(color.a * 255f), (int)(color.r * 255f), (int)(color.g * 255f), (int)(color.b * 255f));
		}

		public static Color ToUnity(this System.Drawing.Color color)
		{
			return new Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
		}

		public static GELMaterial ToUnity(this GELMaterial material)
		{
			Material source = (Material)material.Source;
			material.SetVector4 = (name, vector) => source.SetColor(name, vector.ToColor().ToUnity());
			return material;
		}
	}
}