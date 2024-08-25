using UnityEngine;

namespace Fusyon.GEL
{
    public class UnityPhysics2D : IPhysics
    {
        public IEntity Raycast(System.Numerics.Vector3 origin, System.Numerics.Vector3 direction, float distance = float.PositiveInfinity, int layerMask = 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(origin.X, origin.Y), new Vector2(direction.X, direction.Y), distance);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out IEntity entity))
                {
                    return entity;
                }
            }

            return default;
        }
    }
}