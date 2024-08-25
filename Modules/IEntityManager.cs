using System;

namespace Fusyon.GEL
{
	public interface IEntityManager
	{
		IEntity Create();
		IEntity Clone(IEntity original);
		IEntity Clone(string path);
		void Destroy(IEntity instance);
		void RegisterComponent(IEntity entity, Type type, IEntityComponent component);
		void RegisterComponent<T>(IEntity entity, T component) where T : IEntityComponent;
		T CreateComponent<T>(IEntity entity) where T : IEntityComponent;
		T GetComponent<T>(IEntity entity) where T : IEntityComponent;
		bool TryGetComponent<T>(IEntity entity, out T component) where T : IEntityComponent;
	}
}