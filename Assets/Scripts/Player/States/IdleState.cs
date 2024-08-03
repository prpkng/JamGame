using Game.Input;
using Game.Systems;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Game.Player.States
{
    public class IdleState : State
    {

        public override void Entered(object[] args)
        {
            InputManager.MovePressed += MovePressed;
            PlayerManager.LocalPlayer.rigidbody.isKinematic = true;
        }

        private void MovePressed()
        {
            machine.Switch("walk");
        }

        public override void Exited()
        {
            InputManager.MovePressed -= MovePressed;
            PlayerManager.LocalPlayer.rigidbody.isKinematic = false;
        }

        public override void FixedUpdate()
        {
            PlayerManager.LocalPlayer.PlayerMovementStep(Vector2.zero);
        }

        public override void LateUpdate()
        { }

        public override void Update()
        { }
    }
}