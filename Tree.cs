using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public abstract class Tree
	{
		private enum NodeRequestType
		{
			Create,
			Destroy
		}

		private struct NodeRequest
		{
			public Node Node { get; set; }
			public NodeRequestType Type { get; set; }

			public NodeRequest(Node node, NodeRequestType type)
			{
				Node = node;
				Type = type;
			}
		}

		public Game Game { get; internal set; }

		private bool IsStarted { get; set; }
		private Stack<NodeRequest> Requests { get; set; }
		private List<Node> Nodes { get; set; }

		protected internal Tree()
		{
			Nodes = new List<Node>();
			Requests = new Stack<NodeRequest>();
		}

		protected virtual void OnLoad() { }
		protected virtual void OnUnload() { }

		public void CreateNode(Node node, Node parent = null)
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

		public T CreateNode<T>(Node parent = null) where T : Node
		{
			T script = Activator.CreateInstance<T>();
			CreateNode(script, parent);
			return script;
		}

		public void DestroyNode(Node node)
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

		private void OnCreateNode(Node node)
		{
			node.ID = Nodes.Count;
			node.OnCreate();
			Nodes.Add(node);
		}

		private void OnDestroyNode(Node node)
		{
			node.OnDestroy();
			node.IsDestroyed = true;

			int id = node.ID;
			int lastID = Nodes.Count - 1;

			Node lastNode = Nodes[lastID];
			lastNode.ID = id;
			Nodes[id] = Nodes[lastID];

			Nodes.RemoveAt(lastID);
		}

		private void CreateNodeRequest(Node node, NodeRequestType type)
		{
			Requests.Push(new NodeRequest(node, type));
		}

		private void ProcessNodeRequests()
		{
			while (Requests.Count > 0)
			{
				NodeRequest request = Requests.Pop();
				Node node = request.Node;
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

		private void ProcessNodes(Action<Node> action, bool ignoreDisabled = false)
		{
			foreach (Node node in Nodes)
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