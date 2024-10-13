using Object = UnityEngine.Object;

namespace Fusyon.GEL.Unity
{
    public class GELUnityEntityManager : GELEntityManager
    {
        public GELUnityEntityManager(GELGame game) : base(game)
        {
        }

        protected override T OnClone<T>(T original)
        {
            IGELEntity entity = Object.Instantiate(original as GELEntity);
            return (T)entity;
        }

        protected override void OnDestroy(IGELEntity obj)
        {
            Object.Destroy((obj as GELEntity).gameObject);
        }
    }
}