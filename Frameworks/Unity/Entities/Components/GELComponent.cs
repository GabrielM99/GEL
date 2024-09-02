using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public abstract class GELComponent : MonoBehaviour, IGELComponent
	{
		public GELEntity Entity { get; set; }

		public virtual void OnCreate() { }
		public virtual void OnStart() { }
		public virtual void OnUpdate(float deltaTime) { }
		public virtual void OnFixedUpdate(float deltaTime) { }
		public virtual void OnDestroy() { }
	}

	public abstract class GELComponent<T> : GELComponent where T : Component
	{
		[SerializeField] private T m_Base;

		public T Base { get => m_Base; private set => m_Base = value; }

		protected virtual void Reset()
		{
			Base = GetComponent<T>();
		}

		protected virtual void Awake()
		{
			if (Base == null)
			{
				Base = GetComponent<T>();
			}
		}
	}
}