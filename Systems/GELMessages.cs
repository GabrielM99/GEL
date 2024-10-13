using System;
using System.Collections.Generic;

namespace Fusyon.GEL.Custom
{
	public class GELMessages
	{
		public struct Listener
		{
			public Type Type { get; }
			public object Target { get; }

			public Listener(Type type, object target)
			{
				Type = type;
				Target = target;
			}
		}

		private Dictionary<Listener, List<Action<object>>> CallbackByListener { get; }

		public GELMessages()
		{
			CallbackByListener = new Dictionary<Listener, List<Action<object>>>();
		}

		public virtual void Listen<T>(Action<T> callback, object target = null)
		{
			Listener listener = new Listener(typeof(T), target);

			if (!CallbackByListener.TryGetValue(listener, out List<Action<object>> callbacks))
			{
				callbacks = new List<Action<object>>();
				CallbackByListener[listener] = callbacks;
			}

			callbacks.Add((message) => callback?.Invoke((T)message));
		}

		public virtual void Send(object message, object target = null)
		{
			if (message == null)
			{
				return;
			}

			Listener listener = new Listener(message.GetType(), target);

			if (CallbackByListener.TryGetValue(listener, out List<Action<object>> callbacks))
			{
				foreach (Action<object> callback in callbacks)
				{
					callback?.Invoke(message);
				}
			}
		}
	}
}