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

        public static float HorizontalMoveInput { get; private set; }
        public static float VerticalMoveInput { get; private set; }

        public static bool IsHoldingRun { get;  private set; }

        private void OnActionTriggered(InputAction.CallbackContext obj)
        {
            switch (obj.action.name)
            {
                case "Move":
                    var value = obj.action.ReadValue<Vector2>(); ;
                    HorizontalMoveInput = value.x;
                    VerticalMoveInput = value.y;
                    break;
                case "Run":
                    if (obj.started) IsHoldingRun = true;
                    else if (obj.canceled) IsHoldingRun = false;
                    break;

            }
        }
    }

}