using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public class GELFactory
	{
		public GELGame Game { get; private set; }

		private Dictionary<Type, Func<Type, object>> ProcessorByType { get; set; }

		public GELFactory(GELGame game)
		{
			Game = game;
			ProcessorByType = new Dictionary<Type, Func<Type, object>>();
			AddProcessor<IGELEntity>(CreateEntity);
		}

		protected virtual IGELEntity CreateEntity(Type type)
		{
			IGELEntity entity = (IGELEntity)Activator.CreateInstance(type);
			entity.Game = Game;
			entity.OnCreate();
			return entity;
		}

		public virtual T Create<T>()
		{
			Type type = typeof(T);

			if (TryProcess<T>(type, out object result))
			{
				return (T)result;
			}

			foreach (Type processorType in ProcessorByType.Keys)
			{
				if (processorType.IsAssignableFrom(type))
				{
					if (TryProcess<T>(processorType, out result))
					{
						return (T)result;
					}
				}
			}

			return default;
		}

		public void AddProcessor<T>(Func<Type, object> processor)
		{
			ProcessorByType[typeof(T)] = processor;
		}

		private bool TryProcess<T>(Type type, out object result)
		{
			result = null;

			if (ProcessorByType.TryGetValue(type, out Func<Type, object> processor))
			{
				result = processor?.Invoke(typeof(T));
			}

			return result != null;
		}
	}
}