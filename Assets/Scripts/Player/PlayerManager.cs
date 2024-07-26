using Game.Input;
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



        public bool isDraggingObject;


        public static void PlayerMovementStep(Vector3 inputDirection)
        {
            var rb = CurrentPlayer.rigidbody;
            inputDirection.Normalize();

            float accelRate = MathUtils.FixedDeltaRelativize(PlayerConstants.MOVEMENT_ACCELERATION);

            Vector3 targetSpd = inputDirection.normalized * PlayerConstants.MOVEMENT_SPEED;
            targetSpd.y = rb.velocity.y;
            rb.AddForce((targetSpd - rb.velocity) * accelRate, ForceMode.VelocityChange);
        }
    }
}