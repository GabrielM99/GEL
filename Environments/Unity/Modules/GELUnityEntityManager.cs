using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fusyon.GEL.Unity
{
	public class GELUnityEntityManager : GELEntityManager<GELEntity>
	{
		public GELUnityEntityManager(GELGame game) : base(game)
		{
		}

		public override GELEntity Create()
		{
			return new GameObject().AddComponent<GELEntity>();
		}

		public override GELEntity Clone(GELEntity original)
		{
			GELEntity entity = Object.Instantiate(original);
			entity.name = entity.name.Replace("(Clone)", "");
			return entity;
		}

		public override void Destroy(GELEntity entity)
		{
			Object.Destroy(entity.gameObject);
		}

		protected override void OnCreate(GELEntity entity)
		{
			base.OnCreate(entity);
			RegisterComponents(entity);
		}

		private void RegisterComponents(GELEntity entity)
		{
			foreach (IGELEntityComponent entityComponent in entity.GetComponents<IGELEntityComponent>())
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