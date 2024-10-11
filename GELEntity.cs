using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public class GELEntity
	{
		public bool Enabled { get; internal set; } = true;

		public GELGame Game { get; internal set; }
		public GELScene Scene { get; internal set; }
		public GELEntity Parent { get; internal set; }
		public IGELObject Object { get; private set; }

		internal int ID { get; set; }
		internal bool IsStarted { get; private set; }
		internal bool IsDestroyed { get; set; }

		private string ObjectPath { get; set; }
		private List<GELEntity> Children { get; set; }
		private Dictionary<Type, IGELComponent> ComponentByType { get; set; }

		public GELEntity()
		{
			Children = new List<GELEntity>();
			ComponentByType = new Dictionary<Type, IGELComponent>();
			ObjectPath = GetObjectPath();
		}

		public virtual string GetObjectPath() => null;

		public void CreateObject()
		{
			if (Object == null && ObjectPath != null)
			{
				IGELObject obj = Game.Resources.Load<IGELObject>(ObjectPath);
				Object = Game.ObjectManager.Clone(obj, this);
			}
		}

		public void DestroyObject()
		{
			if (Object != null)
			{
				Game.ObjectManager.Destroy(Object);
			}
		}

		public T CreateChild<T>() where T : GELEntity
		{
			T entity = Scene?.CreateEntity<T>(this);
			entity.Parent = this;
			Children.Add(entity);
			return entity;
		}

		public void RegisterComponent(Type type, IGELComponent component)
		{
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

		public void Enable()
		{
			if (!Enabled)
			{
				foreach (GELEntity entity in Children)
				{
					entity.Enable();
				}

				Enabled = true;
			}
		}

		public void Disable()
		{
			if (Enabled)
			{
				foreach (GELEntity entity in Children)
				{
					entity.Disable();
				}

				Enabled = false;
			}
		}

		public void Destroy()
		{
			if (Object != null)
			{
				Game.ObjectManager.Destroy(Object);
			}

			Scene.DestroyEntity(this);
		}

		internal void Start()
		{
			if (!IsStarted)
			{
				IsStarted = true;
				OnStart();
			}
		}

		protected internal virtual void OnCreate()
		{
			ProcessComponent((component) => component.OnCreate());
			CreateObject();
		}

		protected internal virtual void OnStart()
		{
			ProcessComponent((component) => component.OnStart());
		}

		protected internal virtual void OnUpdate(float deltaTime)
		{
			ProcessComponent((component) => component.OnUpdate(deltaTime));
		}

		protected internal virtual void OnFixedUpdate(float deltaTime)
		{
			ProcessComponent((component) => component.OnFixedUpdate(deltaTime));
		}

		protected internal virtual void OnDestroy()
		{
			ProcessComponent((component) => component.OnDestroy());
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