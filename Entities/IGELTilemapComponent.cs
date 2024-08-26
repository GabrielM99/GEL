using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELTilemap : IGELEntity
	{
		void SetTile(Vector3 position, IGELTile tile);
	}
}