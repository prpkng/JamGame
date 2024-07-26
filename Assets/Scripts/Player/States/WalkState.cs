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

            PlayerManager.PlayerMovementStep(inputDirection);

            if (Mathf.Abs(inputDirection.sqrMagnitude) < Mathf.Epsilon)
            {
                machine.Switch("idle");
                return;
            }

            var transform = PlayerManager.CurrentPlayer.mesh.transform;

            if (PlayerManager.CurrentPlayer.isDraggingObject)
            {
                var dir = PlayerManager.CurrentPlayer.currentDraggingTransform.position - transform.position;
                dir.y = 0;
                dir.Normalize();
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), .25f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputDirection), 0.25f);
            }

        }

        public override void LateUpdate()
        { }

        public override void Update()
        {
        }
    }
}