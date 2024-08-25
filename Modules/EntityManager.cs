using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public abstract class EntityManager<TEntity> : IEntityManager where TEntity : IEntity
	{
		private class EntityComponents
		{
			private Dictionary<Type, IEntityComponent> ComponentByType { get; set; }

			public EntityComponents()
			{
				ComponentByType = new Dictionary<Type, IEntityComponent>();
			}

			public T Create<T>() where T : IEntityComponent
			{
				T component = Activator.CreateInstance<T>();
				Register(component);
				return component;
			}

			public void Register(Type type, IEntityComponent component)
			{
				ComponentByType[type] = component;
			}

			public void Register<T>(T component) where T : IEntityComponent
			{
				Register(typeof(T), component);
			}

			public T Get<T>() where T : IEntityComponent
			{
				return (T)ComponentByType.GetValueOrDefault(typeof(T));
			}

			public bool TryGet<T>(out T component) where T : IEntityComponent
			{
				return (component = Get<T>()) != null;
			}
		}

		private Game Game { get; set; }
		private Dictionary<TEntity, EntityComponents> ComponentsByEntity { get; set; }

		public EntityManager(Game game)
		{
			Game = game;
			ComponentsByEntity = new Dictionary<TEntity, EntityComponents>();
		}

		public virtual TEntity Create() => default;

		IEntity IEntityManager.Create()
		{
			TEntity entity = Create();
			OnCreate(entity);
			return entity;
		}

		public virtual TEntity Clone(TEntity entity) => default;

		public IEntity Clone(IEntity original)
		{
			TEntity entity = Clone((TEntity)original);
			OnCreate(entity);
			return entity;
		}

		public TEntity Clone(string path)
		{
			return (TEntity)Clone(Game.Resources.Load<IEntity>(path));
		}

		IEntity IEntityManager.Clone(string path)
		{
			return Clone(path);
		}

		public virtual void Destroy(TEntity entity) { }

		public void Destroy(IEntity entity)
		{
			Destroy((TEntity)entity);
		}

		public T CreateComponent<T>(TEntity entity) where T : IEntityComponent
		{
			if (!ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				components = new EntityComponents();
				ComponentsByEntity[entity] = components;
			}

			return components.Create<T>();
		}

		public T CreateComponent<T>(IEntity entity) where T : IEntityComponent
		{
			return CreateComponent<T>((TEntity)entity);
		}

		public void RegisterComponent(IEntity entity, Type type, IEntityComponent component)
		{
			RegisterComponent((TEntity)entity, type, component);
		}

		public void RegisterComponent(TEntity entity, Type type, IEntityComponent component)
		{
			if (!ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				components = new EntityComponents();
				ComponentsByEntity[entity] = components;
			}

			components.Register(type, component);
		}

		public void RegisterComponent<T>(TEntity entity, T component) where T : IEntityComponent
		{
			RegisterComponent(entity, typeof(T), component);
		}

		public void RegisterComponent<T>(IEntity entity, T component) where T : IEntityComponent
		{
			RegisterComponent((TEntity)entity, component);
		}

		public T GetComponent<T>(TEntity entity) where T : IEntityComponent
		{
			if (ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				return components.Get<T>();
			}

			return default;
		}

		public T GetComponent<T>(IEntity entity) where T : IEntityComponent
		{
			return GetComponent<T>((TEntity)entity);
		}

		public bool TryGetComponent<T>(TEntity entity, out T component) where T : IEntityComponent
		{
			if (ComponentsByEntity.TryGetValue(entity, out EntityComponents components))
			{
				return components.TryGet(out component);
			}

			component = default;
			return false;
		}

		public bool TryGetComponent<T>(IEntity entity, out T component) where T : IEntityComponent
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