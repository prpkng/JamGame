using Game.Input;
using Game.Player;
using UnityEngine;

namespace Game.Systems.Interaction
{
    public class DragInteractable : Interactable
    {
        [SerializeField] private Collider col;
        [SerializeField] private Rigidbody rb;

        private SpringJoint joint;
        private Vector3 positionOffset;

        public override void Interacted(PlayerInteractor interactor)
        {
            base.Interacted(interactor);
            InputManager.InteractReleased += StopDragging;
            PlayerManager.CurrentPlayer.carryingObject = true;
            positionOffset = PlayerManager.CurrentPlayer.rigidbody.position - rb.position;
            joint = interactor.gameObject.AddComponent<SpringJoint>();
            joint.connectedBody = rb;
            joint.spring = 200f;
            joint.anchor = col.ClosestPointOnBounds(PlayerManager.CurrentPlayer.rigidbody.position);
        }

        private void StopDragging()
        {
            Destroy(joint);
            InputManager.InteractReleased -= StopDragging;
            PlayerManager.CurrentPlayer.carryingObject = false;
        }
    }
}