using UnityEngine;

namespace Fusyon.GEL
{
    public class GELUnityPhysics2D : IGELPhysics
    {
        public IGELEntity Raycast(System.Numerics.Vector3 origin, System.Numerics.Vector3 direction, float distance = float.PositiveInfinity, int layerMask = 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(origin.X, origin.Y), new Vector2(direction.X, direction.Y), distance, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out GELBehaviour behaviour))
                {
                    IGELEntity entity = behaviour.Entity;

                    if (entity != null)
                    {
                        return entity;
                    }
                }
            }

            return default;
        }
    }
}