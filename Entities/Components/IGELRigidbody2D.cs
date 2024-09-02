using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELRigidbody2D : IGELComponent
	{
		void Translate(Vector3 delta);
	}
}