using Game.Input;
using Game.Systems;
using UnityEngine;

namespace Game.Player.States
{
    public class IdleState : State
    {
        public const float deceleration = .1f;

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
            var rb = PlayerManager.CurrentPlayer.rigidbody;
            var inputDirection = new Vector3(InputManager.HorizontalMoveInput, 0, InputManager.VerticalMoveInput);
            inputDirection.Normalize();

            float accelRate = MathUtils.FixedDeltaRelativize(deceleration);

            rb.AddForce(-rb.velocity * accelRate, ForceMode.VelocityChange);
        }

        public override void LateUpdate()
        { }

        public override void Update()
        { }
    }
}