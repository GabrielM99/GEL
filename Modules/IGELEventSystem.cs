using System;

namespace Fusyon.GEL
{
	public interface IGELEventSystem
	{
		void Raise<T>(T e) where T : IGELEvent;
		void Handle<T>(Action<T> handler) where T : IGELEvent;
	}
}