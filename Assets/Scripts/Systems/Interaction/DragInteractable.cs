using Game.Input;
using Game.Player;
using UnityEngine;
using Oddworm.Framework;

namespace Game.Systems.Interaction
{
    public class DragInteractable : Interactable
    {
        [SerializeField] private Collider col;
        [SerializeField] private Rigidbody rb;

        [SerializeField] private PhysicMaterial noFrictionMaterial;

        private ConfigurableJoint joint;

        public override void Interacted(PlayerInteractor interactor)
        {
            base.Interacted(interactor);
            InputManager.InteractReleased += StopDragging;
            PlayerManager.CurrentPlayer.isDraggingObject = true;


            // Configurate Joint
            joint = rb.gameObject.AddComponent<ConfigurableJoint>();
            joint.connectedBody = PlayerManager.CurrentPlayer.rigidbody;
            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;
            joint.angularZMotion = ConfigurableJointMotion.Limited;
            joint.angularZLimit = new SoftJointLimit() { limit = 30 };
            joint.angularYZLimitSpring = new SoftJointLimitSpring() { spring = 100f };
            col.material = noFrictionMaterial;

            joint.anchor = transform.worldToLocalMatrix.MultiplyVector(col.ClosestPointOnBounds(PlayerManager.CurrentPlayer.rigidbody.position) - col.bounds.center); 

            PlayerManager.CurrentPlayer.currentDraggingTransform = rb.transform;
        }

        private void StopDragging()
        {
            PlayerManager.CurrentPlayer.currentDraggingTransform = null;
            Destroy(joint);
            col.material = null;
            InputManager.InteractReleased -= StopDragging;
            PlayerManager.CurrentPlayer.isDraggingObject = false;
        }
    }
}