using System;
using Game.Input;
using Game.Systems;
using Game.Systems.Noise;
using UnityEngine;

namespace Game.Player.States
{
    public class WalkState : State
    {
        public override void Entered(object[] args)
        {
        }

        public override void Exited()
        { }

        public override void FixedUpdate()
        {
            if (InputManager.IsHoldingRun) machine.Switch("run");
            var inputDirection = new Vector3(InputManager.HorizontalMoveInput, 0, InputManager.VerticalMoveInput);

            PlayerManager.LocalPlayer.PlayerMovementStep(inputDirection, PlayerConstants.WALK_SPEED);

            if (Mathf.Abs(inputDirection.sqrMagnitude) < Mathf.Epsilon)
            {
                machine.Switch("idle");
                return;
            }

            var transform = PlayerManager.LocalPlayer.mesh;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputDirection), 0.25f);

            NoiseSystem.Instance.EmitNoise(PlayerConstants.WALK_NOISE_PER_SEC * Time.fixedDeltaTime);
        }

        public override void LateUpdate()
        { }

        public override void Update()
        {
        }
    }
}