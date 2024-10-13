using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[DisallowMultipleComponent]
	public class GELEntity : MonoBehaviour, IGELEntity
	{
		public GELScript Script { get; set; }
		public System.Numerics.Vector3 Position { get => transform.position.ToGEL(); set => transform.position = value.ToUnity(); }
		public System.Numerics.Vector3 Rotation { get => transform.eulerAngles.ToGEL(); set => transform.eulerAngles = value.ToUnity(); }
		public System.Numerics.Vector3 Scale { get => transform.localScale.ToGEL(); set => transform.localEulerAngles = value.ToUnity(); }
		public bool Visible { get => gameObject.activeSelf; set => gameObject.SetActive(value); }
	}
}