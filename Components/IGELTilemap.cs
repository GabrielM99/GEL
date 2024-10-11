using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELTilemap : IGELComponent
	{
		void SetTile(Vector3 position, object tile);
	}
}