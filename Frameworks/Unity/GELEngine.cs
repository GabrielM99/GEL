using System;
using System.IO;
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

        public void AddResource<T>(string virtualPath, string realPath)
        {
            Game.Resources.Add(virtualPath, () => LoadAddressableResource<T>(realPath));
        }

        public void AddResource(string virtualPath, string realPath, bool readAsText = false)
        {
            string fullPath = Path.Combine(Application.dataPath, realPath);
            Game.Resources.Add(virtualPath, () => readAsText ? File.ReadAllText(fullPath) : File.ReadAllBytes(fullPath));
        }

        public void AddResource(string virtualPath, object resource)
        {
            Game.Resources.Add(virtualPath, () => resource);
        }

        public T LoadAddressableResource<T>(string path)
        {
            Type type = typeof(T);

            if (typeof(Component).IsAssignableFrom(type) || type.IsInterface)
            {
                return Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion().GetComponent<T>();
            }

            return Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
        }
    }
}