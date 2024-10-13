using System;
using UnityEngine;

namespace Fusyon.GEL.Unity
{
    [RequireComponent(typeof(Collider2D))]
    public class GELCollider2D : GELBehaviour<Collider2D>, IGELCollider
    {
        public Action<GELCollision> OnCollisionStarted { get; set; }
        public Action<GELCollision> OnCollisionEnded { get; set; }

        public IGELEntity Entity { get; set; }

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnColliderEnter(other.collider);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnColliderExit(other.collider);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnColliderEnter(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnColliderExit(other);
        }

        private void OnColliderEnter(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out IGELCollider gelCollider))
            {
                OnCollisionStarted?.Invoke(new GELCollision(gelCollider));
            }
        }

        private void OnColliderExit(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out IGELCollider gelCollider))
            {
                OnCollisionEnded?.Invoke(new GELCollision(gelCollider));
            }
        }
    }
}