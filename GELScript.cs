using System.Collections.Generic;

namespace Fusyon.GEL
{
	public class GELScript
	{
		public bool Enabled { get; internal set; } = true;

		public GELGame Game { get; internal set; }
		public GELScene Scene { get; internal set; }
		public GELScript Parent { get; internal set; }

		internal int ID { get; set; }
		internal bool IsStarted { get; private set; }
		internal bool IsDestroyed { get; set; }

		private List<GELScript> Children { get; set; }

		public GELScript()
		{
			Children = new List<GELScript>();
		}

		public T CreateChild<T>() where T : GELScript
		{
			T script = Scene?.CreateScript<T>(this);
			script.Parent = this;
			Children.Add(script);
			return script;
		}

		public void Enable()
		{
			if (!Enabled)
			{
				foreach (GELScript script in Children)
				{
					script.Enable();
				}

				Enabled = true;
			}
		}

		public void Disable()
		{
			if (Enabled)
			{
				foreach (GELScript script in Children)
				{
					script.Disable();
				}

				Enabled = false;
			}
		}

		public void Destroy()
		{
			Scene.DestroyScript(this);
		}

		internal void Start()
		{
			if (!IsStarted)
			{
				IsStarted = true;
				OnStart();
			}
		}

		protected internal virtual void OnCreate() { }
		protected internal virtual void OnStart() { }
		protected internal virtual void OnUpdate(float deltaTime) { }
		protected internal virtual void OnFixedUpdate(float deltaTime) { }
		protected internal virtual void OnDestroy() { }
	}
}