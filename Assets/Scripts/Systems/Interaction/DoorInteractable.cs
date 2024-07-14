using Game.Player;
using UnityEngine;

namespace Game.Systems.Interactable
{
    public class DoorInteractable : Interactable
    {
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
        }

        public override void Unhovered(PlayerInteractor interactor) { }
    }
}