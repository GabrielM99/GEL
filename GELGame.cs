using System;
using Fusyon.GEL.Custom;

namespace Fusyon.GEL
{
	public class GELGame
	{
		public GELScene Scene { get; private set; }
		public GELMessages Messages { get; set; }
		public IGELLogger Logger { get; set; }
		public GELResources Resources { get; set; }
		public GELEntityManager EntityManager { get; set; }
		public IGELInput Input { get; set; }
		public IGELPhysics Physics { get; set; }
		public Action<GELScene> OnSceneLoaded { get; set; }

		public GELGame()
		{
			Messages = new GELMessages();
			Resources = new GELResources();
			EntityManager = new GELEntityManager(this);
		}

		public void LoadScene<T>() where T : GELScene
		{
			T scene = Activator.CreateInstance<T>();
			scene.Game = this;
			Scene?.Unload();
			Scene = scene;
			OnSceneLoaded?.Invoke(scene);
			scene.Load();
		}

		public void Update(float deltaTime)
		{
			Scene?.Update(deltaTime);
		}

		public void FixedUpdate(float deltaTime)
		{
			Scene?.FixedUpdate(deltaTime);
		}
	}
}