using System.Numerics;

namespace Fusyon.GEL
{
	public interface IPhysics
	{
		IEntity Raycast(Vector3 origin, Vector3 direction, float distance = float.PositiveInfinity, int layerMask = 0);
	}
}