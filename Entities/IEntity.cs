using System.Numerics;

namespace Fusyon.GEL
{
	public interface IEntity
	{
		Game Game { get; set; }
		Node Node { get; set; }
		Vector3 Position { get; set; }
		Vector3 Rotation { get; set; }
		Vector3 Scale { get; set; }
		bool Visible { get; set; }

		void OnCreate();
		void OnUpdate(float deltaTime);
		void OnFixedUpdate(float deltaTime);
		void Translate(Vector3 delta);
		T CreateEntityComponent<T>() where T : IEntityComponent => Game.EntityManager.CreateComponent<T>(this);
		void RegisterEntityComponent<T>(T component) where T : IEntityComponent => Game.EntityManager.RegisterComponent(this, component);
		T GetEntityComponent<T>() where T : IEntityComponent => Game.EntityManager.GetComponent<T>(this);
		bool TryGetEntityComponent<T>(out T component) where T : IEntityComponent => Game.EntityManager.TryGetComponent(this, out component);
		void OnDestroy();
	}
}