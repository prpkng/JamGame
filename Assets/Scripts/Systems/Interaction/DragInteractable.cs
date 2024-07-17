using Game.Input;
using Game.Player;
using UnityEngine;

namespace Game.Systems.Interaction
{
    public class DragInteractable : Interactable
    {

        [SerializeField] private Rigidbody rb;

        private CharacterJoint joint;

        public override void Interacted(PlayerInteractor interactor)
        {
            base.Interacted(interactor);
            InputManager.InteractReleased += StopDragging;
            joint = interactor.gameObject.AddComponent<CharacterJoint>();
            joint.connectedBody = rb;
            joint.lowTwistLimit = new SoftJointLimit();
            joint.highTwistLimit = new SoftJointLimit();
            joint.swing1Limit = new SoftJointLimit() { limit = 60 };
            joint.swing2Limit = new SoftJointLimit();
            PlayerManager.CurrentPlayer.carryingObject = true;
        }

        private void StopDragging()
        {
            Destroy(joint);
            InputManager.InteractReleased -= StopDragging;
            PlayerManager.CurrentPlayer.carryingObject = false;
        }
    }
}