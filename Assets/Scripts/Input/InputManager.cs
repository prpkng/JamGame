using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Game.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private void Awake()
        {
            playerInput.onActionTriggered += OnActionTriggered;
        }

        public const float GamepadDeadzone = 0.25f;

        public static float HorizontalMoveInput { get; private set; }
        public static float VerticalMoveInput { get; private set; }

        public static bool IsHoldingRun { get; private set; }

        public static event Action InteractPressed;
        public static event Action InteractReleased;

        private void OnActionTriggered(InputAction.CallbackContext obj)
        {
            switch (obj.action.name)
            {
                case "Interact":
                    if (obj.started)
                        InteractPressed?.Invoke();
                    else if (obj.canceled)
                        InteractReleased?.Invoke();

                    break;
                case "Move":
                    var value = obj.action.ReadValue<Vector2>(); ;
                    HorizontalMoveInput = Mathf.Abs(value.x) > GamepadDeadzone ? value.x : 0;
                    VerticalMoveInput = Mathf.Abs(value.y) > GamepadDeadzone ? value.y : 0;
                    break;
                case "Run":
                    if (obj.started) IsHoldingRun = true;
                    else if (obj.canceled) IsHoldingRun = false;
                    break;

            }
        }
    }

}