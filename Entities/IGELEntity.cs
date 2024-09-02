using System;

namespace Fusyon.GEL
{
	public interface IGELEntity : IGELObject, IGELActor
	{
		GELGame Game { get; set; }
		GELNode Node { get; set; }

		void RegisterComponent(Type type, IGELComponent component);
		T GetComponent<T>() where T : IGELComponent;
		bool TryGetComponent<T>(out T component) where T : IGELComponent;
	}
}