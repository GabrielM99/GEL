using System;
using System.Collections.Generic;
using System.Numerics;

namespace Fusyon.GEL
{
	public class GELEntity : IGELEntity
	{
		public virtual Vector3 Position { get; set; }
		public virtual Vector3 Rotation { get; set; }
		public virtual Vector3 Scale { get; set; }
		public virtual bool Visible { get; set; }

		public GELGame Game { get; set; }
		public GELNode Node { get; set; }

		private Dictionary<Type, IGELComponent> ComponentByType { get; set; }

		public GELEntity()
		{
			ComponentByType = new Dictionary<Type, IGELComponent>();
		}

		public virtual void OnCreate()
		{
			ProcessComponent((component) => component.OnCreate());
		}

		public virtual void OnStart()
		{
			ProcessComponent((component) => component.OnStart());
		}

		public virtual void OnUpdate(float deltaTime)
		{
			ProcessComponent((component) => component.OnUpdate(deltaTime));
		}

		public virtual void OnFixedUpdate(float deltaTime)
		{
			ProcessComponent((component) => component.OnFixedUpdate(deltaTime));
		}

		public virtual void OnDestroy()
		{
			ProcessComponent((component) => component.OnDestroy());
		}

		public void RegisterComponent(Type type, IGELComponent component)
		{
			component.Entity = this;
			ComponentByType[type] = component;
		}

		public T GetComponent<T>() where T : IGELComponent
		{
			return (T)ComponentByType.GetValueOrDefault(typeof(T));
		}

		public bool TryGetComponent<T>(out T component) where T : IGELComponent
		{
			return (component = GetComponent<T>()) != null;
		}

		private void ProcessComponent(Action<IGELComponent> action)
		{
			foreach (IGELComponent component in ComponentByType.Values)
			{
				action?.Invoke(component);
			}
		}
	}
}