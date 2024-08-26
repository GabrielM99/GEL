using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELEntity : IGELResource
	{
		GELGame Game { get; set; }
		GELNode Node { get; set; }
		Vector3 Position { get; set; }
		Vector3 Rotation { get; set; }
		Vector3 Scale { get; set; }
		bool Visible { get; set; }

		void OnCreate();
		void OnUpdate(float deltaTime);
		void OnFixedUpdate(float deltaTime);
		void Translate(Vector3 delta);
		T CreateGELComponent<T>() where T : IGELEntityComponent => Game.EntityManager.CreateComponent<T>(this);
		void RegisterGELComponent<T>(T component) where T : IGELEntityComponent => Game.EntityManager.RegisterComponent(this, component);
		T GetGELComponent<T>() where T : IGELEntityComponent => Game.EntityManager.GetComponent<T>(this);
		bool TryGetGELComponent<T>(out T component) where T : IGELEntityComponent => Game.EntityManager.TryGetComponent(this, out component);
		void OnDestroy();
	}
}