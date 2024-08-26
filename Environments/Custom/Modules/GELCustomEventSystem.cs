using System;
using System.Collections.Generic;

namespace Fusyon.GEL.Custom
{
	public class GELCustomEventSystem : IGELEventSystem
	{
		private Dictionary<Type, List<Action<IGELEvent>>> HandlersByType { get; set; }

		public GELCustomEventSystem()
		{
			HandlersByType = new Dictionary<Type, List<Action<IGELEvent>>>();
		}

		public void Raise<T>(T e) where T : IGELEvent
		{
			Type type = typeof(T);

			if (HandlersByType.TryGetValue(type, out List<Action<IGELEvent>> handlers))
			{
				foreach (Action<IGELEvent> listener in handlers)
				{
					listener?.Invoke(e);
				}
			}
		}

		public void Handle<T>(Action<T> handler) where T : IGELEvent
		{
			Type type = typeof(T);

			if (!HandlersByType.TryGetValue(type, out List<Action<IGELEvent>> handlers))
			{
				handlers = new List<Action<IGELEvent>>();
				HandlersByType[type] = handlers;
			}

			handlers.Add((message) => handler?.Invoke((T)message));
		}
	}
}