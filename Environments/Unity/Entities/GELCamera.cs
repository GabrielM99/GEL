using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[RequireComponent(typeof(Camera))]
	public class GELCamera : GELEntity<Camera>, ICameraEntity
	{
		public float Zoom { get => Base.fieldOfView; set => Base.fieldOfView = value; }
	}
}