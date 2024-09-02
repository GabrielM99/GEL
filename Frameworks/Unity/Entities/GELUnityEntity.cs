using System.Numerics;

namespace Fusyon.GEL.Unity
{
	public abstract class GELUnityEntity : GELUnityEntity<IGELBehaviour> { }

	public abstract class GELUnityEntity<T> : GELEntity where T : IGELBehaviour
	{
		private T m_Object;

		public override Vector3 Position { get => Behaviour.Position; set => Behaviour.Position = value; }
		public override Vector3 Rotation { get => Behaviour.Rotation; set => Behaviour.Rotation = value; }
		public override Vector3 Scale { get => Behaviour.Scale; set => Behaviour.Scale = value; }
		public override bool Visible { get => Behaviour.Visible; set => Behaviour.Visible = value; }

		protected T Behaviour
		{
			get
			{
				return m_Object;
			}

			set
			{
				if (value != null && !value.Equals(m_Object))
				{
					value.Bind(this);
				}

				m_Object = value;
			}
		}
	}
}