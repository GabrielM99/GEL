using System.Collections.Generic;

namespace Fusyon.GEL
{
	public class Node
	{
		public bool IsEnabled { get; private set; } = true;

		public Game Game { get; internal set; }
		public Tree Tree { get; internal set; }
		public Node Parent { get; internal set; }

		internal int ID { get; set; }
		internal bool IsDestroyed { get; set; }

		private List<Node> Children { get; set; }

		public Node()
		{
			Children = new List<Node>();
		}

		protected internal virtual void OnCreate() { }
		protected internal virtual void OnEnable() { }
		protected internal virtual void OnUpdate(float deltaTime) { }
		protected internal virtual void OnFixedUpdate(float deltaTime) { }
		protected internal virtual void OnDisable() { }
		protected internal virtual void OnDestroy() { }

		public T CreateChild<T>() where T : Node
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

				foreach (Node node in Children)
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

				foreach (Node node in Children)
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
	}
}