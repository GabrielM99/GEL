using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public class GELResources
	{
		private Dictionary<string, Func<object>> LoaderByPath { get; }

		public GELResources()
		{
			LoaderByPath = new Dictionary<string, Func<object>>();
		}

		public void Add(string path, Func<object> loader)
		{
			LoaderByPath[path] = loader;
		}

		public T Load<T>(string path)
		{
			return (T)LoaderByPath[path]?.Invoke();
		}
	}
}