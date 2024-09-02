using System;
using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public class GELUnityFactory : GELFactory
	{
		public GELUnityFactory(GELGame game) : base(game)
		{
		}

		protected override IGELEntity CreateEntity(Type type)
		{
			return (IGELEntity)new GameObject().AddComponent(type);
		}
	}
}