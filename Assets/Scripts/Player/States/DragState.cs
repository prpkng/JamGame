using System;
using Game.Input;
using Game.Systems;
using UnityEngine;

namespace Game.Player.States
{
    public class DragState : State
    {
        public override void Entered(object[] args)
        { }

        public override void Exited()
        { }

        public override void FixedUpdate()
        {
            var inputDirection = new Vector3(InputManager.HorizontalMoveInput, 0, InputManager.VerticalMoveInput);

            PlayerManager.LocalPlayer.PlayerMovementStep(inputDirection);

            var transform = PlayerManager.LocalPlayer.mesh.transform;

            var dir = PlayerManager.LocalPlayer.currentDraggingTransform.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), .25f);

        }

        public override void LateUpdate()
        { }

        public override void Update()
        {
        }
    }
}