using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELRigidbody2D : IGELComponent
	{
		Vector3 LinearVelocity { get; set; }
		Vector3 AngularVelocity { get; set; }

		void Translate(Vector3 delta);
	}
}