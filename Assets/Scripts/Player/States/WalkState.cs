using System;
using Game.Input;
using Game.Systems;
using UnityEngine;

namespace Game.Player.States
{
    public class WalkState : State
    {
        public const float acceleration = .1f;
        public const float speed = 7f;
        public override void Entered(object[] args)
        { }

        public override void Exited()
        { }

        public override void FixedUpdate()
        {
            var rb = PlayerManager.CurrentPlayer.rigidbody;
            var inputDirection = new Vector3(InputManager.HorizontalMoveInput, 0, InputManager.VerticalMoveInput);
            inputDirection.Normalize();

            float accelRate = MathUtils.FixedDeltaRelativize(acceleration);

            Vector3 targetSpd = inputDirection.normalized * speed;
            targetSpd.y = rb.velocity.y;
            rb.AddForce((targetSpd - rb.velocity) * accelRate, ForceMode.VelocityChange);

            if (Mathf.Abs(inputDirection.sqrMagnitude) < Mathf.Epsilon)
            {
                machine.Switch("idle");
                return;
            }

            var transform = PlayerManager.CurrentPlayer.transform;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputDirection), 0.25f);
        }

        public override void LateUpdate()
        { }

        public override void Update()
        {
        }
    }
}