using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Fusyon.GEL.Unity
{
    public class GELUnityAdressableResources : IGELResources
    {
        public T Load<T>(string path) where T : IGELResource
        {
            object asset = Addressables.LoadAssetAsync<object>(path).WaitForCompletion();

            if (asset is GameObject gameObject)
            {
                Type type = typeof(T);

                if (typeof(Component).IsAssignableFrom(type) || type.IsInterface)
                {
                    return gameObject.GetComponent<T>();
                }
            }

            return (T)asset;
        }
    }
}