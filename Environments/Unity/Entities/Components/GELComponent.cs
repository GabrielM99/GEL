using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public abstract class GELComponent<T> : MonoBehaviour where T : Component
	{
		[SerializeField] private T m_Base;

		protected T Base { get => m_Base; private set => m_Base = value; }

		protected virtual void Reset()
		{
			Base = GetComponent<T>();
		}
	}
}