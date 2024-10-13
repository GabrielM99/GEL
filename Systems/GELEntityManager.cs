using System;
using System.Collections.Generic;

namespace Fusyon.GEL
{
    public class GELEntityManager
    {
        private GELGame Game { get; }
        private List<IGELEntity> Entities { get; }

        public GELEntityManager(GELGame game)
        {
            Game = game;
            Entities = new List<IGELEntity>();
            game.OnSceneLoaded += OnSceneLoaded;
        }

        public T Create<T>(T original, GELScript script = null) where T : IGELEntity
        {
            T obj = OnClone(original);
            obj.Script = script;
            Entities.Add(obj);
            return obj;
        }

        public IGELEntity Create(IGELEntity original, GELScript script = null)
        {
            return Create<IGELEntity>(original, script);
        }

        public T Create<T>(string resourcePath, GELScript script = null) where T : IGELEntity
        {
            return Create(Game.Resources.Load<T>(resourcePath), script);
        }

        public IGELEntity Create(string resourcePath, GELScript script = null)
        {
            return Create(Game.Resources.Load<IGELEntity>(resourcePath), script);
        }

        public void Destroy(IGELEntity obj)
        {
            OnDestroy(obj);
            obj.Script = null;
            Entities.Remove(obj);
        }

        protected virtual T OnClone<T>(T original) where T : IGELEntity
        {
            T obj = (T)Activator.CreateInstance(original.GetType());
            return obj;
        }

        protected virtual void OnDestroy(IGELEntity obj) { }

        private void OnSceneLoaded(GELScene scene)
        {
            for (int i = Entities.Count - 1; i >= 0; i--)
            {
                Destroy(Entities[i]);
            }
        }
    }
}