using Game.Input;
using Game.Systems;
using UnityEngine;

namespace Game.Player.States
{
    public class IdleState : State
    {

        public override void Entered(object[] args)
        {
            InputManager.MovePressed += MovePressed;
        }

        private void MovePressed()
        {
            machine.Switch("walk");
        }

        public override void Exited()
        {
            InputManager.MovePressed -= MovePressed;
        }

        public override void FixedUpdate()
        {
            PlayerManager.CurrentPlayer.PlayerMovementStep(Vector2.zero);
        }

        public override void LateUpdate()
        { }

        public override void Update()
        { }
    }
}