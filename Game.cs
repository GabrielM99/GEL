using System;
using Fusyon.GEL.Custom;

namespace Fusyon.GEL
{
	public class Game
	{
		public Tree Tree { get; private set; }
		public ILogger Logger { get; set; }
		public IResources Resources { get; set; }
		public IEntityManager EntityManager { get; set; }
		public IInput Input { get; set; }
		public IEventSystem EventSystem { get; set; }
		public IPhysics Physics { get; set; }

		public Game()
		{
			EntityManager = new CustomEntityManager(this);
			EventSystem = new CustomEventSystem();
		}

		public void LoadTree<T>() where T : Tree
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