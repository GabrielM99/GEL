using System.Numerics;

namespace Fusyon.GEL.Custom
{
	public class GELCustomEntity : IGELEntity
	{
		public virtual Vector3 Position { get; set; }
		public virtual Vector3 Rotation { get; set; }
		public virtual Vector3 Scale { get; set; }
		public virtual bool Visible { get; set; }

		public GELGame Game { get; set; }
		public GELNode Node { get; set; }

		public virtual void OnCreate() { }
		public virtual void OnUpdate(float deltaTime) { }
		public virtual void OnFixedUpdate(float deltaTime) { }
		public virtual void Translate(Vector3 delta) => Position += delta;
		public virtual void OnDestroy() { }
	}
}