using System;

namespace Fusyon.GEL.Custom
{
	public class GELCustomEntityManager : GELEntityManager<GELCustomEntity>
	{
		public GELCustomEntityManager(GELGame game) : base(game)
		{
		}

		public override GELCustomEntity Create()
		{
			return Activator.CreateInstance<GELCustomEntity>();
		}
	}
}