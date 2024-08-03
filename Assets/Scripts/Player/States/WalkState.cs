using System;
using Game.Input;
using Game.Systems;
using UnityEngine;

namespace Game.Player.States
{
    public class WalkState : State
    {
        public override void Entered(object[] args)
        { }

        public override void Exited()
        { }

        public override void FixedUpdate()
        {
            var inputDirection = new Vector3(InputManager.HorizontalMoveInput, 0, InputManager.VerticalMoveInput);

            PlayerManager.LocalPlayer.PlayerMovementStep(inputDirection);

            if (Mathf.Abs(inputDirection.sqrMagnitude) < Mathf.Epsilon)
            {
                machine.Switch("idle");
                return;
            }

            var transform = PlayerManager.LocalPlayer.mesh.transform;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputDirection), 0.25f);

        }

        public override void LateUpdate()
        { }

        public override void Update()
        {
        }
    }
}