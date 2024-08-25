using System;
using System.Collections.Generic;

namespace Fusyon.GEL.Custom
{
	public class CustomEventSystem : IEventSystem
	{
		private Dictionary<Type, List<Action<IEvent>>> HandlersByType { get; set; }

		public CustomEventSystem()
		{
			HandlersByType = new Dictionary<Type, List<Action<IEvent>>>();
		}

		public void Raise<T>(T e) where T : IEvent
		{
			Type type = typeof(T);

			if (HandlersByType.TryGetValue(type, out List<Action<IEvent>> handlers))
			{
				foreach (Action<IEvent> listener in handlers)
				{
					listener?.Invoke(e);
				}
			}
		}

		public void Handle<T>(Action<T> handler) where T : IEvent
		{
			Type type = typeof(T);

			if (!HandlersByType.TryGetValue(type, out List<Action<IEvent>> handlers))
			{
				handlers = new List<Action<IEvent>>();
				HandlersByType[type] = handlers;
			}

			handlers.Add((message) => handler?.Invoke((T)message));
		}
	}
}