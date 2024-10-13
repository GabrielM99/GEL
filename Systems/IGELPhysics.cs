using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELPhysics
	{
		IGELEntity Raycast(Vector3 origin, Vector3 direction, float distance = float.PositiveInfinity, int layerMask = int.MaxValue);
	}
}