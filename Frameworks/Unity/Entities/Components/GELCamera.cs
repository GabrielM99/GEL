using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[RequireComponent(typeof(Camera))]
	public class GELCamera : GELComponent<Camera>, IGELCamera
	{
		public float Zoom { get => Base.fieldOfView; set => Base.fieldOfView = value; }
		public System.Numerics.Vector2 Size { get => Base.pixelRect.size.ToGEL(); set => Base.pixelRect = new Rect(Base.pixelRect.position, value.ToUnity()); }
		public System.Numerics.Matrix4x4 ProjectionMatrix { get => Base.projectionMatrix.ToGEL(); set => Base.projectionMatrix = value.ToUnity(); }
		public System.Numerics.Matrix4x4 ViewMatrix { get => Base.worldToCameraMatrix.ToGEL(); set => Base.worldToCameraMatrix = value.ToUnity(); }
		public float Near { get => Base.nearClipPlane; set => Base.nearClipPlane = value; }
	}
}