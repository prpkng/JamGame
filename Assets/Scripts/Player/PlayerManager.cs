using Game.Input;
using Oddworm.Framework;
using UnityEngine;

namespace Game.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager CurrentPlayer;

        public MeshRenderer mesh;

        [System.NonSerialized] public PlayerInteractor interactor;
        [System.NonSerialized] public PlayerStateMachine playerStates;
        [System.NonSerialized] public new Rigidbody rigidbody;

        private void Awake()
        {
            CurrentPlayer = this;
            interactor = GetComponent<PlayerInteractor>();
            rigidbody = GetComponent<Rigidbody>();
            playerStates = GetComponent<PlayerStateMachine>();
        }


        public void StartDragging()
        {
            playerStates.machine.Switch("drag");
        }

        public void StopDragging()
        {
            if (Mathf.Abs(InputManager.HorizontalMoveInput) +
                Mathf.Abs(InputManager.VerticalMoveInput) > 0)
                playerStates.machine.Switch("walk");
            else
                playerStates.machine.Switch("idle");
        }
        public Transform currentDraggingTransform;


        public void PlayerMovementStep(Vector3 inputDirection)
        {
            var rb = rigidbody;
            inputDirection.Normalize();

            float accelRate = MathUtils.FixedDeltaRelativize(
                Mathf.Abs(inputDirection.sqrMagnitude) < Mathf.Epsilon
                ? PlayerConstants.MOVEMENT_DECELERATION
                : PlayerConstants.MOVEMENT_ACCELERATION);

            Vector3 targetSpd = inputDirection.normalized * PlayerConstants.MOVEMENT_SPEED;
            targetSpd.y = rb.velocity.y;
            rb.AddForce((targetSpd - rb.velocity) * accelRate, ForceMode.VelocityChange);
        }
    }
}