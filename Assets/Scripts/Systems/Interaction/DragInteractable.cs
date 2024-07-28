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
            PlayerManager.CurrentPlayer.StartDragging();


            // Configurate Joint
            joint = rb.gameObject.AddComponent<ConfigurableJoint>();
            joint.connectedBody = PlayerManager.CurrentPlayer.rigidbody;
            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;
            col.material = noFrictionMaterial;

            var target = PlayerManager.CurrentPlayer.rigidbody.position;
            target.y = col.bounds.center.y;
            joint.anchor = transform.worldToLocalMatrix.MultiplyVector(target - col.bounds.center);

            PlayerManager.CurrentPlayer.currentDraggingTransform = rb.transform;
        }

        private void StopDragging()
        {
            PlayerManager.CurrentPlayer.currentDraggingTransform = null;
            Destroy(joint);
            col.material = null;
            InputManager.InteractReleased -= StopDragging;
            PlayerManager.CurrentPlayer.StopDragging();
        }
    }
}