using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELCamera : IGELComponent
	{
		float Zoom { get; set; }
		Vector2 Size { get; set; }
		Matrix4x4 ProjectionMatrix { get; set; }
		Matrix4x4 ViewMatrix { get; set; }
		float Near { get; set; }

		public Vector3 ScreenToWorldPosition(Vector3 position)
		{
			Matrix4x4 viewProjection = ViewMatrix * ProjectionMatrix;
			Matrix4x4.Invert(viewProjection, out Matrix4x4 inverseViewProjection);
			Vector4 ndc = new Vector4(2f * (position.X / Size.X) - 1f, 2f * (position.Y / Size.Y) - 1f, Near, 1f);
			Vector4 worldPosition = Vector4.Transform(ndc, inverseViewProjection);

			float w = 1f / worldPosition.W;

			if (w != 0f)
			{
				return new Vector3(worldPosition.X * w, worldPosition.Y * w, worldPosition.Z * w);
			}

			return new Vector3(worldPosition.X, worldPosition.Y, worldPosition.Z);
		}

		public Vector3 WorldToScreenPosition(Vector3 position)
		{
			Matrix4x4 viewProjection = ViewMatrix * ProjectionMatrix;
			Vector4 worldPositionHomogeneous = new Vector4(position, 1f);
			Vector4 ndc = Vector4.Transform(worldPositionHomogeneous, viewProjection);

			float w = ndc.W;

			if (w != 0f)
			{
				ndc /= w;
			}

			return new Vector3((ndc.X * 0.5f + 0.5f) * Size.X, (ndc.Y * 0.5f + 0.5f) * Size.Y, ndc.Z * 0.5f + 0.5f); ;
		}
	}
}