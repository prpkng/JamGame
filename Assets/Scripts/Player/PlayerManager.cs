using Game.Input;
using Oddworm.Framework;
using UnityEngine;

namespace Game.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager LocalPlayer;

        public MeshRenderer mesh;

        [System.NonSerialized] public PlayerInventoryManager inventory;
        [System.NonSerialized] public PlayerInteractor interactor;
        [System.NonSerialized] public PlayerStateMachine playerStates;
        [System.NonSerialized] public new Rigidbody rigidbody;
        [System.NonSerialized] public new CapsuleCollider collider;


        [SerializeField] private LayerMask groundMask;

        private void Awake()
        {
            LocalPlayer = this;
            interactor = GetComponent<PlayerInteractor>();
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();
            playerStates = GetComponent<PlayerStateMachine>();
            inventory = GetComponent<PlayerInventoryManager>();
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



            if (Physics.Raycast(rb.position, Vector3.down, out RaycastHit hit, 1.6f, groundMask))
            {
                inputDirection = Vector3.Lerp(inputDirection, Vector3.Reflect(inputDirection, hit.normal), .5f);
                rb.velocity = inputDirection.normalized * PlayerConstants.MOVEMENT_SPEED;

                if (!Physics.CheckBox(rb.position, new Vector3(collider.radius, .1f, collider.radius), Quaternion.identity, groundMask))
                {
                    print("Not grounded!");
                    rb.position = new Vector3(rb.position.x, hit.point.y, rb.position.z);
                }
            }
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