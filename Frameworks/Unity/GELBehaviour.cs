using System;
using System.Linq;
using Fusyon.GEL.Unity;
using UnityEngine;

namespace Fusyon.GEL
{
	public class GELBehaviour : GELBehaviour<IGELEntity> { }

	[DisallowMultipleComponent]
	public class GELBehaviour<T> : MonoBehaviour, IGELBehaviour where T : IGELEntity
	{
		[SerializeField] private GELComponent[] m_Components;

		public T Entity { get; private set; }
		IGELEntity IGELBehaviour.Entity { get => Entity; set => Entity = (T)value; }
		public System.Numerics.Vector3 Position { get => transform.position.ToGEL(); set => transform.position = value.ToUnity(); }
		public System.Numerics.Vector3 Rotation { get => transform.eulerAngles.ToGEL(); set => transform.eulerAngles = value.ToUnity(); }
		public System.Numerics.Vector3 Scale { get => transform.localScale.ToGEL(); set => transform.localEulerAngles = value.ToUnity(); }
		public bool Visible { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

		private GELComponent[] Components { get => m_Components; set => m_Components = value; }

		private void Reset()
		{
			Components = GetComponents<GELComponent>();
		}

		private void Awake()
		{
			if (Components.Length == 0)
			{
				Components = GetComponents<GELComponent>();
			}
		}

		public virtual void Bind(T entity)
		{
			Entity = entity;

			foreach (IGELComponent component in Components)
			{
				Type[] interfaceTypes = component.GetType().GetInterfaces();
				Type[] minInterfaceTypes = interfaceTypes.Except(interfaceTypes.SelectMany(t => t.GetInterfaces())).ToArray();

				foreach (Type interfaceType in minInterfaceTypes)
				{
					entity.RegisterComponent(interfaceType, component);
				}
			}
		}

		void IGELBehaviour.Bind(IGELEntity entity)
		{
			Bind((T)entity);
		}
	}
}