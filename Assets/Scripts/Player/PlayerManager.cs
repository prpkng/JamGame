using UnityEngine;

namespace Game.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager CurrentPlayer;

        public PlayerInteractor interactor;
        public ThirdPersonMovement movement;

        private void Awake()
        {
            CurrentPlayer = this;
            interactor = GetComponent<PlayerInteractor>();
            movement = GetComponent<ThirdPersonMovement>();
        }


        private bool isPlayerCarryingObject;

        public bool carryingObject { get => isPlayerCarryingObject; set
            {
                isPlayerCarryingObject = value;
                movement.faceRotationSpeed = value ? Mathf.Infinity : 0.025f;
            }
        }
    }
}