using Game.Player;
using Oddworm.Framework;
using UnityEngine;

namespace Game.Systems.Interaction
{
    public class DoorInteractable : Interactable
    {

        private void Update()
        {
            gameObject.layer = INTERACTABLE_LAYER;
        }

        [SerializeField] private Rigidbody rb;
        [SerializeField] private float doorOpenForce = 10;
        public override void Hovered(PlayerInteractor interactor) { }

        public override void Interacted(PlayerInteractor interactor)
        {
            rb.isKinematic = false;
            Vector3 dir = transform.position - interactor.transform.position;
            dir.y = 0;
            dir.Normalize();
            rb.AddForce(dir * doorOpenForce, ForceMode.Impulse);
            Destroy(gameObject);
        }

        public override void Unhovered(PlayerInteractor interactor) { }
    }
}