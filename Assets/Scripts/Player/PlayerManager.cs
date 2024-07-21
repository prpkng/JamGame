using UnityEngine;

namespace Game.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager CurrentPlayer;

        public PlayerInteractor interactor;
        public new Rigidbody rigidbody;

        private void Awake()
        {
            CurrentPlayer = this;
            interactor = GetComponent<PlayerInteractor>();
            rigidbody = GetComponent<Rigidbody>();
        }


        private bool isPlayerCarryingObject;

        public bool carryingObject
        {
            get => isPlayerCarryingObject; set
            {
                isPlayerCarryingObject = value;
                // movement.faceMovement = !value;
            }
        }
    }
}