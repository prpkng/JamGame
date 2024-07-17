using Cysharp.Threading.Tasks.Triggers;
using Game.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Player
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        public Rigidbody rb;
        public float speed;
        public float acceleration = 0;
        public float deceleration = 0;
        public float faceRotationSpeed = 0f;
        public bool faceMovement;

        private Vector3 inputDirection;

        
        private float targetAngle;

        private void Update()
        {
            if (!faceMovement) return;
            if (inputDirection.sqrMagnitude == 0) return;
            var euler = transform.eulerAngles;
            euler.y = Mathf.LerpAngle(euler.y, 90f-Mathf.Atan2(inputDirection.z, inputDirection.x) * Mathf.Rad2Deg, Time.deltaTime * faceRotationSpeed);
            transform.eulerAngles = euler;
        }

        void FixedUpdate()
        {
            inputDirection = new Vector3(InputManager.HorizontalMoveInput, 0, InputManager.VerticalMoveInput);
            inputDirection.Normalize();

            float accelRate = MathUtils.FixedDeltaRelativize(inputDirection.sqrMagnitude == 0 ? deceleration : acceleration);

            Vector3 targetSpd = inputDirection.normalized * speed;
            targetSpd.y = rb.velocity.y;
            rb.AddForce((targetSpd - rb.velocity) * accelRate, ForceMode.VelocityChange);
        }
    }

}