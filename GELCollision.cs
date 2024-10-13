namespace Fusyon.GEL
{
    public class GELCollision
    {
        public IGELCollider Collider { get; }

        public GELCollision(IGELCollider collider)
        {
            Collider = collider;
        }
    }
}