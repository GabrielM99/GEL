using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public abstract class GELScene
	{
		private enum EntityRequestType
		{
			Create,
			Destroy
		}

		private struct EntityRequest
		{
			public GELEntity Entity { get; }
			public EntityRequestType Type { get; }

			public EntityRequest(GELEntity entity, EntityRequestType type)
			{
				Entity = entity;
				Type = type;
			}
		}

		public GELGame Game { get; internal set; }
		public Action<GELEntity> OnEntityCreated { get; set; }
		public Action<GELEntity> OnEntityDestroyed { get; set; }

		private bool IsStarted { get; set; }
		private Stack<EntityRequest> Requests { get; set; }
		private List<GELEntity> Entities { get; set; }
		private Stack<GELEntity> UnstartedEntities { get; set; }

		protected internal GELScene()
		{
			Entities = new List<GELEntity>();
			UnstartedEntities = new Stack<GELEntity>();
			Requests = new Stack<EntityRequest>();
		}

		protected virtual void OnLoad() { }
		protected virtual void OnUnload() { }

		public void CreateEntity(GELEntity entity, GELEntity parent = null)
		{
			entity.Game = Game;
			entity.Scene = this;
			entity.Parent = parent;

			if (IsStarted)
			{
				CreateEntityRequest(entity, EntityRequestType.Create);
			}
			else
			{
				OnCreateEntity(entity);
			}
		}

		public T CreateEntity<T>(GELEntity parent = null) where T : GELEntity
		{
			T script = Activator.CreateInstance<T>();
			CreateEntity(script, parent);
			return script;
		}

		public void DestroyEntity(GELEntity entity)
		{
			if (entity.IsDestroyed)
			{
				return;
			}

			if (IsStarted)
			{
				CreateEntityRequest(entity, EntityRequestType.Destroy);
			}
			else
			{
				OnDestroyEntity(entity);
			}
		}

		internal void Load()
		{
			OnLoad();
			IsStarted = true;
		}

		internal void Update(float deltaTime)
		{
			ProcessEntityRequests();
			ProcessUnstartedEntities();
			ProcessEntities((entitiy) => entitiy.OnUpdate(deltaTime), true);
		}

		internal void FixedUpdate(float deltaTime)
		{
			ProcessUnstartedEntities();
			ProcessEntities((entity) => entity.OnFixedUpdate(deltaTime), true);
		}

		internal void Unload()
		{
			ProcessEntities((entity) => OnDestroyEntity(entity, false));
			OnUnload();
			IsStarted = false;
		}

		private void OnCreateEntity(GELEntity entity)
		{
			entity.ID = Entities.Count;
			OnEntityCreated?.Invoke(entity);
			entity.OnCreate();
			UnstartedEntities.Push(entity);
			Entities.Add(entity);
		}

		private void OnDestroyEntity(GELEntity entity, bool cleanUp = true)
		{
			OnEntityDestroyed?.Invoke(entity);
			entity.OnDestroy();

			if (cleanUp)
			{
				entity.IsDestroyed = true;

				int id = entity.ID;
				int lastID = Entities.Count - 1;

				GELEntity lastEntity = Entities[lastID];
				lastEntity.ID = id;
				Entities[id] = Entities[lastID];

				Entities.RemoveAt(lastID);
			}
		}

		private void CreateEntityRequest(GELEntity entity, EntityRequestType type)
		{
			Requests.Push(new EntityRequest(entity, type));
		}

		private void ProcessEntityRequests()
		{
			while (Requests.Count > 0)
			{
				EntityRequest request = Requests.Pop();
				GELEntity entity = request.Entity;

				switch (request.Type)
				{
					case EntityRequestType.Create:
						OnCreateEntity(entity);
						break;
					case EntityRequestType.Destroy:
						OnDestroyEntity(entity);
						break;
				}
			}
		}

		private void ProcessUnstartedEntities()
		{
			while (UnstartedEntities.Count > 0)
			{
				GELEntity entity = UnstartedEntities.Pop();
				entity.Start();
			}
		}

		private void ProcessEntities(Action<GELEntity> action, bool ignoreDisabled = false)
		{
			foreach (GELEntity entity in Entities)
			{
				if (ignoreDisabled && !entity.Enabled)
				{
					continue;
				}

				action?.Invoke(entity);
			}
		}
	}
}