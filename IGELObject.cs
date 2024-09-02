using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELObject
	{
		Vector3 Position { get; set; }
		Vector3 Rotation { get; set; }
		Vector3 Scale { get; set; }
		bool Visible { get; set; }
	}
}