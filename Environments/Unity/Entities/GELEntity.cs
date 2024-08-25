using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public class GELEntity : MonoBehaviour, IEntity
	{
		public Game Game { get; set; }
		public Node Node { get; set; }
		public System.Numerics.Vector3 Position { get => new System.Numerics.Vector3(transform.position.x, transform.position.y, transform.position.z); set => transform.position = new Vector2(value.X, value.Y); }
		public System.Numerics.Vector3 Rotation { get => new System.Numerics.Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z); set => transform.eulerAngles = new Vector3(value.X, value.Y, value.Z); }
		public System.Numerics.Vector3 Scale { get => new System.Numerics.Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z); set => transform.localScale = new Vector3(value.X, value.Y, value.Z); }
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