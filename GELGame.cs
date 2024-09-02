using System;
using Fusyon.GEL.Custom;

namespace Fusyon.GEL
{
	public class GELGame
	{
		public GELTree Tree { get; private set; }
		public GELFactory Factory { get; set; }
		public GELEvents Events { get; set; }
		public IGELLogger Logger { get; set; }
		public IGELResources Resources { get; set; }
		public IGELInput Input { get; set; }
		public IGELPhysics Physics { get; set; }

		public GELGame()
		{
			Factory = new GELFactory(this);
			Events = new GELEvents();
		}

		public void LoadTree<T>() where T : GELTree
		{
			T tree = Activator.CreateInstance<T>();
			tree.Game = this;
			Tree?.Unload();
			Tree = tree;
			tree.Load();
		}

		public void Update(float deltaTime)
		{
			Tree?.Update(deltaTime);
		}

		public void FixedUpdate(float deltaTime)
		{
			Tree?.FixedUpdate(deltaTime);
		}
	}
}