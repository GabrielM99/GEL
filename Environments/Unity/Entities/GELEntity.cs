using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public class GELEntity : MonoBehaviour, IGELEntity
	{
		public GELGame Game { get; set; }
		public GELNode Node { get; set; }
		public System.Numerics.Vector3 Position { get => transform.position.ToGEL(); set => transform.position = value.ToUnity(); }
		public System.Numerics.Vector3 Rotation { get => transform.eulerAngles.ToGEL(); set => transform.eulerAngles = value.ToUnity(); }
		public System.Numerics.Vector3 Scale { get => transform.localScale.ToGEL(); set => transform.localScale = value.ToUnity(); }
		public bool Visible { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

		public virtual void OnCreate() { }
		public virtual void OnUpdate(float deltaTime) { }
		public virtual void OnFixedUpdate(float deltaTime) { }
		public virtual void Translate(System.Numerics.Vector3 delta) => Position += delta;
		public virtual void OnDestroy() { }
	}

	public class GELEntity<T> : GELEntity where T : Component
	{
		[SerializeField] private T m_Base;

		protected T Base { get => m_Base; private set => m_Base = value; }

		protected virtual void Reset()
		{
			Base = GetComponent<T>();
		}
	}
}