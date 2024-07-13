using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float acceleration = 0;
    [SerializeField] private float deceleration = 0;

    void Start()
    {
        
    }

    private Vector3 velocity;

    void Update()
    {
        var inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputDirection.Normalize();

        var yVel = velocity.y;


        var accelRate = inputDirection.sqrMagnitude == 0 ? deceleration : acceleration;

        if (accelRate == 0)
            accelRate = 1;
        else
            accelRate = Time.deltaTime / accelRate;

        var targetSpd = inputDirection.normalized * speed;
        velocity = Vector3.Lerp(velocity, targetSpd, accelRate);

        velocity.y = yVel;

        velocity += gravity * Time.deltaTime * Vector3.down;

        character.Move(velocity * Time.deltaTime);

    }
}


// 100ms each frame

// 1s speed

// t = 1 * 0.1