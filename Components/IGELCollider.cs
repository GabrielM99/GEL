using System;

namespace Fusyon.GEL
{
    public interface IGELCollider
    {
        Action<GELCollision> OnCollisionStarted { get; set; }
        Action<GELCollision> OnCollisionEnded { get; set; }

        IGELEntity Entity { get; set; }
    }
}