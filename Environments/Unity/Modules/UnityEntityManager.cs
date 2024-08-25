using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fusyon.GEL.Unity
{
	public class UnityEntityManager : EntityManager<GELEntity>
	{
		public UnityEntityManager(Game game) : base(game)
		{
		}

		public override GELEntity Create()
		{
			return new GameObject().AddComponent<GELEntity>();
		}

		public override GELEntity Clone(GELEntity original)
		{
			return Object.Instantiate(original);
		}

		public override void Destroy(GELEntity entity)
		{
			Object.Destroy(entity.gameObject);
		}

		protected override void OnCreate(GELEntity entity)
		{
			base.OnCreate(entity);

			foreach (IEntityComponent entityComponent in entity.GetComponents<IEntityComponent>())
			{
				Type[] interfaceTypes = entityComponent.GetType().GetInterfaces();
				Type[] minInterfaceTypes = interfaceTypes.Except(interfaceTypes.SelectMany(t => t.GetInterfaces())).ToArray();

				foreach (Type interfaceType in minInterfaceTypes)
				{
					RegisterComponent(entity, interfaceType, entityComponent);
				}
			}
		}
	}
}