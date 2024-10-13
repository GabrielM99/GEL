using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public class GELResources
	{
		private Dictionary<string, Func<object>> LoaderByPath { get; }
		private Dictionary<string, object> ResourceByPath { get; }

		public GELResources()
		{
			LoaderByPath = new Dictionary<string, Func<object>>();
			ResourceByPath = new Dictionary<string, object>();
		}

		public void Add(string path, Func<object> loader)
		{
			LoaderByPath[path] = loader;
		}

		public T Load<T>(string path)
		{
			if (!ResourceByPath.TryGetValue(path, out object resource))
			{
				resource = LoaderByPath[path]?.Invoke();
				ResourceByPath[path] = resource;
			}

			return (T)resource;
		}
	}
}