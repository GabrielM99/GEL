using Fusyon.GEL.Unity;
using UnityEngine;

namespace Fusyon.GEL
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class GELRigidbody2D : GELBehaviour<Rigidbody2D>, IGELRigidbody
	{
		public System.Numerics.Vector3 LinearVelocity { get => ((Vector3)Base.velocity).ToGEL(); set => Base.velocity = value.ToUnity(); }
		public System.Numerics.Vector3 AngularVelocity { get => new Vector3(0f, 0f, Base.angularVelocity).ToGEL(); set => Base.angularVelocity = value.ToUnity().z; }

		public void Translate(System.Numerics.Vector3 delta)
		{
			Base.MovePosition((Vector3)Base.position + delta.ToUnity());
		}
	}
}