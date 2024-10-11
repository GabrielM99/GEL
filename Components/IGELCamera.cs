using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELCamera : IGELComponent
	{
		Vector3 ScreenToWorldPosition(Vector3 position);
		Vector3 WorldToScreenPosition(Vector3 position);
	}
}