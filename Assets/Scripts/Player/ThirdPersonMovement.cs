using Game.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed;
        [SerializeField] private float acceleration = 0;
        [SerializeField] private float deceleration = 0;

        [SerializeField] private float faceRotationSpeed = 0f;


        private Vector3 inputDirection;

        private float targetAngle;

        private void Update()
        {
            Vector3 euler = transform.eulerAngles;
            euler.y = Mathf.LerpAngle(euler.y, 90-targetAngle, MathUtils.DeltaRelativize(faceRotationSpeed));
            transform.eulerAngles = euler;
        }

        void FixedUpdate()
        {
            inputDirection = new Vector3(InputManager.HorizontalMoveInput, 0, InputManager.VerticalMoveInput);
            if (inputDirection.sqrMagnitude != 0)
                targetAngle = Mathf.Atan2(inputDirection.z, inputDirection.x) * Mathf.Rad2Deg;
            inputDirection.Normalize();

            float accelRate = MathUtils.FixedDeltaRelativize(inputDirection.sqrMagnitude == 0 ? deceleration : acceleration);

            Vector3 targetSpd = inputDirection.normalized * speed;
            targetSpd.y = rb.velocity.y;
            rb.velocity = Vector3.Lerp(rb.velocity, targetSpd, accelRate);
        }
    }

}