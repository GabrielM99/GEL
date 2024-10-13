using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public abstract class GELBehaviour<T> : MonoBehaviour where T : Component
	{
		[SerializeField] private T _Base;

		public T Base { get => _Base; private set => _Base = value; }

		protected virtual void Reset()
		{
			Base = GetComponent<T>();
		}

		protected virtual void Awake()
		{
			// TODO: Save value in editor.
			if (Base == null)
			{
				Base = GetComponent<T>();
			}
		}
	}
}