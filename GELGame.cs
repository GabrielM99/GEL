using System;
using Fusyon.GEL.Custom;

namespace Fusyon.GEL
{
	public class GELGame
	{
		public GELTree Tree { get; private set; }
		public IGELLogger Logger { get; set; }
		public IGELResources Resources { get; set; }
		public IGELEntityManager EntityManager { get; set; }
		public IGELInput Input { get; set; }
		public IGELEventSystem EventSystem { get; set; }
		public IGELPhysics Physics { get; set; }

		public GELGame()
		{
			EntityManager = new GELCustomEntityManager(this);
			EventSystem = new GELCustomEventSystem();
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