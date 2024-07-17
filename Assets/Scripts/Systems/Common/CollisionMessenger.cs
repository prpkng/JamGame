using System;
using UnityEngine;

namespace Game.Systems.Common
{
    public class CollisionMessenger : MonoBehaviour
    {
        public event Action<Collider> EnteredHitbox;
        public event Action<Collider> ExitedHitbox;
        public bool isInsideHitbox;

        public bool trigger;
        public string targetTag = "";
        public LayerMask targetMask = Physics.AllLayers;


        private void OnCollisionEnter(Collision collision)
        {
            if (trigger) return;
            if ((targetMask & (1 << collision.gameObject.layer)) == 0) return;
            EnteredHitbox?.Invoke(collision.collider);
            isInsideHitbox = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!trigger) return;
            if ((targetMask & (1 << other.gameObject.layer)) == 0) return;
            EnteredHitbox?.Invoke(other);
            isInsideHitbox = true;
        }
        private void OnCollisionExit(Collision collision)
        {
            if (trigger) return;
            if ((targetMask & (1 << collision.gameObject.layer)) == 0) return;
            ExitedHitbox?.Invoke(collision.collider);
            isInsideHitbox = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!trigger) return;
            if ((targetMask & (1 << other.gameObject.layer)) == 0) return;
            ExitedHitbox?.Invoke(other);
            isInsideHitbox = false;
        }
    }
}