using System.Collections.Generic;

namespace Fusyon.GEL
{
	public class GELNode<T> : GELNode where T : IGELEntity
	{
		private T m_Entity;

		public T Entity
		{
			get => m_Entity;

			set
			{
				if (value != null)
				{
					if (value.Equals(m_Entity))
					{
						return;
					}

					value.Node = this;
				}

				m_Entity?.OnDestroy();
				m_Entity = value;
			}
		}

		protected internal override void OnCreate()
		{
			base.OnCreate();
			Entity?.OnCreate();
		}

		protected internal override void OnEnable()
		{
			base.OnEnable();

			if (Entity != null)
			{
				Entity.Visible = true;
			}
		}

		protected internal override void OnUpdate(float deltaTime)
		{
			base.OnUpdate(deltaTime);
			Entity?.OnUpdate(deltaTime);
		}

		protected internal override void OnFixedUpdate(float deltaTime)
		{
			base.OnFixedUpdate(deltaTime);
			Entity?.OnFixedUpdate(deltaTime);
		}

		protected internal override void OnDisable()
		{
			base.OnDisable();

			if (Entity != null)
			{
				Entity.Visible = false;
			}
		}

		protected internal override void OnDestroy()
		{
			base.OnDestroy();
			Entity?.OnDestroy();
		}
	}

	public class GELNode
	{
		public bool IsEnabled { get; private set; } = true;

		public GELGame Game { get; internal set; }
		public GELTree Tree { get; internal set; }
		public GELNode Parent { get; internal set; }

		internal int ID { get; set; }
		internal bool IsStarted { get; private set; }
		internal bool IsDestroyed { get; set; }

		private List<GELNode> Children { get; set; }

		public GELNode()
		{
			Children = new List<GELNode>();
		}

		protected internal virtual void OnCreate() { }
		protected internal virtual void OnStart() { }
		protected internal virtual void OnEnable() { }
		protected internal virtual void OnUpdate(float deltaTime) { }
		protected internal virtual void OnFixedUpdate(float deltaTime) { }
		protected internal virtual void OnDisable() { }
		protected internal virtual void OnDestroy() { }

		public T CreateChildNode<T>() where T : GELNode
		{
			T node = Tree?.CreateNode<T>(this);
			Children.Add(node);
			return node;
		}

		public void Toggle()
		{
			if (IsEnabled)
			{
				Disable();
			}
			else
			{
				Enable();
			}
		}

		public void Enable()
		{
			if (!IsEnabled)
			{
				OnEnable();

				foreach (GELNode node in Children)
				{
					node.Enable();
				}

				IsEnabled = true;
			}
		}

		public void Disable()
		{
			if (IsEnabled)
			{
				OnDisable();

				foreach (GELNode node in Children)
				{
					node.Disable();
				}

				IsEnabled = false;
			}
		}

		public void Destroy()
		{
			Tree.DestroyNode(this);
		}

		internal void Start()
		{
			if (!IsStarted)
			{
				IsStarted = true;
				OnStart();
			}
		}
	}
}