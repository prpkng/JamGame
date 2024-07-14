using System.Collections;
using Game.Input;
using Game.Systems.Interactable;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Handles all sorts of player interactions.
    /// 
    /// Uses a simple boxcast checking for occurrences of the <see cref="Interactable"/> class
    /// </summary>
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private float maxInteractDistance = 1.5f;

        private void OnEnable() => InputManager.InteractPressed += OnInteractPressed;
        private void OnDisable() => InputManager.InteractPressed += OnInteractPressed;

        private void OnInteractPressed()
        {
            if (lastHitInfo.collider != null &&
                lastHitInfo.collider.TryGetComponent(out Interactable interactable))
            {
                interactable.Interacted(this);
            }
        }

        private void HandleHovering(RaycastHit hitInfo)
        {
            // When the collider is null and was NOT null, send the UNHOVER message
            if (hitInfo.collider == null &&
                lastHitInfo.collider != null && 
                lastHitInfo.collider.TryGetComponent(out Interactable interactable))
            {
                interactable.Unhovered(this);
            }

            // When the collider is not null, and WAS indeed null,  send the HOVER message
            if (hitInfo.collider != null && 
                hitInfo.collider != lastHitInfo.collider &&
                hitInfo.collider.TryGetComponent(out interactable))
            {
                interactable.Hovered(this);
            }
        }

        private RaycastHit lastHitInfo;
        private void FixedUpdate()
        {
            Physics.Raycast(transform.position,
                            transform.forward,
                            out RaycastHit hitInfo,
                            maxInteractDistance,
                            interactionLayer);

            HandleHovering(hitInfo);

            lastHitInfo = hitInfo;
        }
    }
}