using System;

namespace Fusyon.GEL
{
	public interface IEventSystem
	{
		void Raise<T>(T e) where T : IEvent;
		void Handle<T>(Action<T> handler) where T : IEvent;
	}
}