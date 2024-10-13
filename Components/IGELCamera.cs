using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELCamera
	{
		Vector3 ScreenToWorldPosition(Vector3 position);
		Vector3 WorldToScreenPosition(Vector3 position);
	}
}