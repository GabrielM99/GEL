using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[RequireComponent(typeof(Camera))]
	public class GELCamera : GELComponent<Camera>, IGELCamera
	{
		public System.Numerics.Vector3 ScreenToWorldPosition(System.Numerics.Vector3 position)
		{
			return Base.ScreenToWorldPoint(position.ToUnity()).ToGEL();
		}

		public System.Numerics.Vector3 WorldToScreenPosition(System.Numerics.Vector3 position)
		{
			return Base.WorldToScreenPoint(position.ToUnity()).ToGEL();
		}
	}
}