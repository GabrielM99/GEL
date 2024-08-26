using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public abstract class GELEntityManager<TEntity> : IGELEntityManager where TEntity : IGELEntity
	{
		private class EntityComponents
		{
			private Dictionary<Type, IGELEntityComponent> ComponentByType { get; set; }

			public EntityComponents()
			{
				ComponentByType = new Dictionary<Type, IGELEntityComponent>();
			}

			public T Create<T>() where T : IGELEntityComponent
			{
				T component = Activator.CreateInstance<T>();
				Register(component);
				return component;
			}

			public void Register(Type type, IGELEntityComponent component)
			{
				ComponentByType[type] = component;
			}

			public void Register<T>(T component) where T : IGELEntityComponent
			{
				Register(typeof(T), component);
			}

			public T Get<T>() where T : IGELEntityComponent
			{
				return (T)ComponentByType.GetValueOrDefault(typeof(T));
			}

			public bool TryGet<T>(out T component) where T : IGELEntityComponent
			{
				return (component = Get<T>()) != null;
			}
		}

		private GELGame Game { get; set; }
		private Dictionary<TEntity, EntityComponents> ComponentsByEntity { get; set; }

		public GELEntityManager(GELGame game)
		{
			Game = game;
			ComponentsByEntity = new Dictionary<TEntity, EntityComponents>();
		}

		public virtual TEntity Create() => default;

		IGELEntity IGELEntityManager.Create()
		{
			TEntity entity = Create();
			OnCreate(entity);
			return entity;
		}

		public virtual TEntity Clone(TEntity entity) => default;

		public IGELEntity Clone(IGELEntity original)
		{
			TEntity entity = Clone((TEntity)original);
			OnCreate(entity);
			return entity;
		}

		public TEntity Clone(string path)
		{
			return (TEntity)Clone(Game.Resources.Load<IGELEntity>(path));
		}

		IGELEntity IGELEntityManager.Clone(string path)
		{
			return Clone(path);
		}

		public virtual void Destroy(TEntity entity) { }

		public void Destroy(IGELEntity entity)
		{
			Destroy((TEntity)entity);
		}

		public T CreateComponent<T>(TEntity entity) where T : IGELEntityComponent
		{
			if (!ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				components = new EntityComponents();
				ComponentsByEntity[entity] = components;
			}

			return components.Create<T>();
		}

		public T CreateComponent<T>(IGELEntity entity) where T : IGELEntityComponent
		{
			return CreateComponent<T>((TEntity)entity);
		}

		public void RegisterComponent(IGELEntity entity, Type type, IGELEntityComponent component)
		{
			RegisterComponent((TEntity)entity, type, component);
		}

		public void RegisterComponent(TEntity entity, Type type, IGELEntityComponent component)
		{
			if (!ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				components = new EntityComponents();
				ComponentsByEntity[entity] = components;
			}

			components.Register(type, component);
		}

		public void RegisterComponent<T>(TEntity entity, T component) where T : IGELEntityComponent
		{
			RegisterComponent(entity, typeof(T), component);
		}

		public void RegisterComponent<T>(IGELEntity entity, T component) where T : IGELEntityComponent
		{
			RegisterComponent((TEntity)entity, component);
		}

		public T GetComponent<T>(TEntity entity) where T : IGELEntityComponent
		{
			if (ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				return components.Get<T>();
			}

			return default;
		}

		public T GetComponent<T>(IGELEntity entity) where T : IGELEntityComponent
		{
			return GetComponent<T>((TEntity)entity);
		}

		public bool TryGetComponent<T>(TEntity entity, out T component) where T : IGELEntityComponent
		{
			if (ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				return components.TryGet(out component);
			}

			component = default;
			return false;
		}

		public bool TryGetComponent<T>(IGELEntity entity, out T component) where T : IGELEntityComponent
		{
			return TryGetComponent((TEntity)entity, out component);
		}

		protected virtual void OnCreate(TEntity entity)
		{
			entity.Game = Game;
			entity.OnCreate();
		}
	}
}