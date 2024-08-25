using System;

namespace Fusyon.GEL.Custom
{
	public class CustomEntityManager : EntityManager<Entity>
	{
		public CustomEntityManager(Game game) : base(game)
		{
		}

		public override Entity Create()
		{
			return Activator.CreateInstance<Entity>();
		}
	}
}