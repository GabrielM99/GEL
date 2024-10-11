using System;
using System.Linq;
using Object = UnityEngine.Object;

namespace Fusyon.GEL.Unity
{
    public class GELUnityObjectManager : GELObjectManager
    {
        protected override IGELObject OnClone(IGELObject original, GELEntity entity = null)
        {
            GELObject obj = Object.Instantiate((GELObject)original);

            if (entity != null)
            {
                foreach (IGELComponent component in obj.Components)
                {
                    Type[] interfaceTypes = component.GetType().GetInterfaces();
                    Type[] minInterfaceTypes = interfaceTypes.Except(interfaceTypes.SelectMany(t => t.GetInterfaces())).ToArray();

                    foreach (Type interfaceType in minInterfaceTypes)
                    {
                        entity.RegisterComponent(interfaceType, component);
                    }
                }
            }

            return obj;
        }

        protected override void OnDestroy(IGELObject obj)
        {
            Object.Destroy((obj as GELObject).gameObject);
        }
    }
}