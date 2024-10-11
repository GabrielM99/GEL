using Fusyon.GEL.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Fusyon.GEL
{
	[RequireComponent(typeof(Tilemap))]
	public class GELTilemap : GELComponent<Tilemap>, IGELTilemap
	{
		public void SetTile(System.Numerics.Vector3 position, object tile)
		{
			Base.SetTile(Vector3Int.FloorToInt(new Vector3(position.X, position.Y, position.Z)), tile as TileBase);
		}
	}
}