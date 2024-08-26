using System;

namespace Fusyon.GEL
{
	public interface IGELEntityManager
	{
		IGELEntity Create();
		IGELEntity Clone(IGELEntity original);
		IGELEntity Clone(string path);
		void Destroy(IGELEntity instance);
		void RegisterComponent(IGELEntity entity, Type type, IGELEntityComponent component);
		void RegisterComponent<T>(IGELEntity entity, T component) where T : IGELEntityComponent;
		T CreateComponent<T>(IGELEntity entity) where T : IGELEntityComponent;
		T GetComponent<T>(IGELEntity entity) where T : IGELEntityComponent;
		bool TryGetComponent<T>(IGELEntity entity, out T component) where T : IGELEntityComponent;
	}
}