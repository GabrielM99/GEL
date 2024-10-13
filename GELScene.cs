using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
	public abstract class GELScene
	{
		private enum ScriptRequestType
		{
			Create,
			Destroy
		}

		private struct ScriptRequest
		{
			public GELScript Script { get; }
			public ScriptRequestType Type { get; }

			public ScriptRequest(GELScript script, ScriptRequestType type)
			{
				Script = script;
				Type = type;
			}
		}

		public GELGame Game { get; internal set; }
		public Action<GELScript> OnScriptCreated { get; set; }
		public Action<GELScript> OnScriptDestroyed { get; set; }

		private bool IsStarted { get; set; }
		private Stack<ScriptRequest> Requests { get; set; }
		private List<GELScript> Scripts { get; set; }
		private Stack<GELScript> UnstartedScripts { get; set; }

		protected internal GELScene()
		{
			Scripts = new List<GELScript>();
			UnstartedScripts = new Stack<GELScript>();
			Requests = new Stack<ScriptRequest>();
		}

		protected virtual void OnLoad() { }
		protected virtual void OnUnload() { }

		public void CreateScript(GELScript script, GELScript parent = null)
		{
			script.Game = Game;
			script.Scene = this;
			script.Parent = parent;

			if (IsStarted)
			{
				CreateScriptRequest(script, ScriptRequestType.Create);
			}
			else
			{
				OnCreateScript(script);
			}
		}

		public T CreateScript<T>(GELScript parent = null) where T : GELScript
		{
			T script = Activator.CreateInstance<T>();
			CreateScript(script, parent);
			return script;
		}

		public void DestroyScript(GELScript script)
		{
			if (script.IsDestroyed)
			{
				return;
			}

			if (IsStarted)
			{
				CreateScriptRequest(script, ScriptRequestType.Destroy);
			}
			else
			{
				OnDestroyScript(script);
			}
		}

		internal void Load()
		{
			OnLoad();
			IsStarted = true;
		}

		internal void Update(float deltaTime)
		{
			ProcessScriptRequests();
			ProcessUnstartedScripts();
			ProcessScripts((script) => script.OnUpdate(deltaTime), true);
		}

		internal void FixedUpdate(float deltaTime)
		{
			ProcessUnstartedScripts();
			ProcessScripts((script) => script.OnFixedUpdate(deltaTime), true);
		}

		internal void Unload()
		{
			ProcessScripts((script) => OnDestroyScript(script, false));
			OnUnload();
			IsStarted = false;
		}

		private void OnCreateScript(GELScript script)
		{
			script.ID = Scripts.Count;
			OnScriptCreated?.Invoke(script);
			script.OnCreate();
			UnstartedScripts.Push(script);
			Scripts.Add(script);
		}

		private void OnDestroyScript(GELScript script, bool cleanUp = true)
		{
			OnScriptDestroyed?.Invoke(script);
			script.OnDestroy();

			if (cleanUp)
			{
				script.IsDestroyed = true;

				int id = script.ID;
				int lastID = Scripts.Count - 1;

				GELScript lastScript = Scripts[lastID];
				lastScript.ID = id;
				Scripts[id] = Scripts[lastID];

				Scripts.RemoveAt(lastID);
			}
		}

		private void CreateScriptRequest(GELScript script, ScriptRequestType type)
		{
			Requests.Push(new ScriptRequest(script, type));
		}

		private void ProcessScriptRequests()
		{
			while (Requests.Count > 0)
			{
				ScriptRequest request = Requests.Pop();
				GELScript script = request.Script;

				switch (request.Type)
				{
					case ScriptRequestType.Create:
						OnCreateScript(script);
						break;
					case ScriptRequestType.Destroy:
						OnDestroyScript(script);
						break;
				}
			}
		}

		private void ProcessUnstartedScripts()
		{
			while (UnstartedScripts.Count > 0)
			{
				GELScript script = UnstartedScripts.Pop();
				script.Start();
			}
		}

		private void ProcessScripts(Action<GELScript> action, bool ignoreDisabled = false)
		{
			foreach (GELScript script in Scripts)
			{
				if (ignoreDisabled && !script.Enabled)
				{
					continue;
				}

				action?.Invoke(script);
			}
		}
	}
}
