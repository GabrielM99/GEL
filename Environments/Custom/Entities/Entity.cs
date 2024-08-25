using System.Numerics;

namespace Fusyon.GEL.Custom
{
	public class Entity : IEntity
	{
		public Game Game { get; set; }
		public Node Node { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Scale { get; set; }
		public bool Visible { get; set; }

		public virtual void OnCreate() { }
		public virtual void OnUpdate(float deltaTime) { }
		public virtual void OnFixedUpdate(float deltaTime) { }
		public virtual void Translate(Vector3 delta) => Position += delta;
		public virtual void OnDestroy() { }
	}
}