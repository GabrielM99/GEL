using UnityEngine;

namespace Fusyon.GEL.Unity
{
    public class GELUnityPhysics2D : IGELPhysics
    {
        public IGELEntity Raycast(System.Numerics.Vector3 origin, System.Numerics.Vector3 direction, float distance = float.PositiveInfinity, int layerMask = int.MaxValue)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin.ToUnity(), direction.ToUnity(), distance);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out GELEntity entity))
                {
                    return entity;
                }
            }

            return default;
        }
    }
}