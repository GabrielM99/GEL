using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Fusyon.GEL.Unity
{
    public abstract class GELEngine : MonoBehaviour
    {
        protected GELGame Game { get; private set; }

        protected virtual void Awake()
        {
            Game = new GELGame();
        }

        protected virtual void Update()
        {
            Game.Update(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            Game.FixedUpdate(Time.fixedDeltaTime);
        }

        public void AddResource<T>(string originalPath, string virtualPath)
        {
            Game.Resources.Add(virtualPath, () => LoadAddressableResource<T>(originalPath));
        }

        public void AddResourceLoader(string virtualPath, object resource)
        {
            Game.Resources.Add(virtualPath, () => resource);
        }

        public T LoadAddressableResource<T>(string path)
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