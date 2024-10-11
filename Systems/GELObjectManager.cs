using System;

namespace Fusyon.GEL
{
    public class GELObjectManager
    {
        public IGELObject Clone(IGELObject original, GELEntity entity = null)
        {
            IGELObject obj = OnClone(original, entity);
            obj.Entity = entity;
            return obj;
        }

        public void Destroy(IGELObject obj)
        {
            OnDestroy(obj);
        }

        protected virtual IGELObject OnClone(IGELObject original, GELEntity entity = null)
        {
            IGELObject obj = (IGELObject)Activator.CreateInstance(original.GetType());
            return obj;
        }

        protected virtual void OnDestroy(IGELObject obj) { }
    }
}