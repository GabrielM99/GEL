using Fusyon.GEL.Unity;
using UnityEngine;

namespace Fusyon.GEL
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class GELRigidbody2D : GELComponent<Rigidbody2D>, IGELRigidbody2D
	{
		public void Translate(System.Numerics.Vector3 delta)
		{
			Base.MovePosition((Vector3)Base.position + delta.ToUnity());
		}
	}
}