using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public abstract class GELTree
	{
		private enum NodeRequestType
		{
			Create,
			Destroy
		}

		private struct NodeRequest
		{
			public GELNode Node { get; set; }
			public NodeRequestType Type { get; set; }

			public NodeRequest(GELNode node, NodeRequestType type)
			{
				Node = node;
				Type = type;
			}
		}

		public GELGame Game { get; internal set; }

		private bool IsStarted { get; set; }
		private Stack<NodeRequest> Requests { get; set; }
		private List<GELNode> Nodes { get; set; }

		protected internal GELTree()
		{
			Nodes = new List<GELNode>();
			Requests = new Stack<NodeRequest>();
		}

		protected virtual void OnLoad() { }
		protected virtual void OnUnload() { }

		public void CreateNode(GELNode node, GELNode parent = null)
		{
			node.Game = Game;
			node.Tree = this;
			node.Parent = parent;

			if (IsStarted)
			{
				CreateNodeRequest(node, NodeRequestType.Create);
			}
			else
			{
				OnCreateNode(node);
			}
		}

		public T CreateNode<T>(GELNode parent = null) where T : GELNode
		{
			T script = Activator.CreateInstance<T>();
			CreateNode(script, parent);
			return script;
		}

		public void DestroyNode(GELNode node)
		{
			if (node.IsDestroyed)
			{
				return;
			}

			if (IsStarted)
			{
				CreateNodeRequest(node, NodeRequestType.Destroy);
			}
			else
			{
				OnDestroyNode(node);
			}
		}

		internal void Load()
		{
			OnLoad();
			IsStarted = true;
		}

		internal void Update(float deltaTime)
		{
			ProcessNodeRequests();
			ProcessNodes((node) => node.OnUpdate(deltaTime), true);
		}

		internal void FixedUpdate(float deltaTime)
		{
			ProcessNodes((node) => node.OnFixedUpdate(deltaTime), true);
		}

		internal void Unload()
		{
			ProcessNodes((node) => node.OnDestroy());
			OnUnload();
			IsStarted = false;
		}

		private void OnCreateNode(GELNode node)
		{
			node.ID = Nodes.Count;
			node.OnCreate();
			Nodes.Add(node);
		}

		private void OnDestroyNode(GELNode node)
		{
			node.OnDestroy();
			node.IsDestroyed = true;

			int id = node.ID;
			int lastID = Nodes.Count - 1;

			GELNode lastNode = Nodes[lastID];
			lastNode.ID = id;
			Nodes[id] = Nodes[lastID];

			Nodes.RemoveAt(lastID);
		}

		private void CreateNodeRequest(GELNode node, NodeRequestType type)
		{
			Requests.Push(new NodeRequest(node, type));
		}

		private void ProcessNodeRequests()
		{
			while (Requests.Count > 0)
			{
				NodeRequest request = Requests.Pop();
				GELNode node = request.Node;
				NodeRequestType type = request.Type;

				if (type == NodeRequestType.Create)
				{
					OnCreateNode(node);
				}
				else if (type == NodeRequestType.Destroy)
				{
					OnDestroyNode(node);
				}
			}
		}

		private void ProcessNodes(Action<GELNode> action, bool ignoreDisabled = false)
		{
			foreach (GELNode node in Nodes)
			{
				if (ignoreDisabled && !node.IsEnabled)
				{
					continue;
				}

				action?.Invoke(node);
			}
		}
	}
}