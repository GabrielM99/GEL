using System;
using System.Collections.Generic;

namespace Fusyon.GEL.Custom
{
	public class GELEvents
	{
		private Dictionary<Type, List<Action<object>>> HandlersByType { get; set; }

		public GELEvents()
		{
			HandlersByType = new Dictionary<Type, List<Action<object>>>();
		}

		public void Raise<T>(T e)
		{
			Type type = typeof(T);

			if (HandlersByType.TryGetValue(type, out List<Action<object>> handlers))
			{
				foreach (Action<object> listener in handlers)
				{
					listener?.Invoke(e);
				}
			}
		}

		public void Handle<T>(Action<T> handler)
		{
			Type type = typeof(T);

			if (!HandlersByType.TryGetValue(type, out List<Action<object>> handlers))
			{
				handlers = new List<Action<object>>();
				HandlersByType[type] = handlers;
			}

			handlers.Add((message) => handler?.Invoke((T)message));
		}
	}
}