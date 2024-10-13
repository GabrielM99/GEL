using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELEntity
	{
		GELScript Script { get; set; }
		Vector3 Position { get; set; }
		Vector3 Rotation { get; set; }
		Vector3 Scale { get; set; }
		bool Visible { get; set; }
	}
}