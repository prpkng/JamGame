using System.Collections;
using Game.Systems.Common;
using Game.Input;
using Game.Systems.Interaction;
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
        [SerializeField] private CollisionMessenger collisionMessenger;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private float maxInteractDistance = 1.5f;

        private void OnEnable()
        {
            InputManager.InteractPressed += OnInteractPressed;
            collisionMessenger.EnteredHitbox += EnteredCollision;
            collisionMessenger.ExitedHitbox += ExitedCollision;
        }

        private void ExitedCollision(Collider collider)
        {
            if (!collider.TryGetComponent(out Interactable interactable))
                return;

            interactable.Unhovered(this);
            target = null;
            print("Exited!");
        }

        private void EnteredCollision(Collider collider)
        {
            if (!collider.TryGetComponent(out Interactable interactable))
                return;

            interactable.Hovered(this);
            target = collider;
            print("Entered!"); 
        }

        private Collider target;

        private void OnDisable()
        {
            InputManager.InteractPressed -= OnInteractPressed;
            collisionMessenger.EnteredHitbox -= EnteredCollision;
            collisionMessenger.ExitedHitbox -= ExitedCollision;
        }

        private void OnInteractPressed()
        {
            if (!collisionMessenger.isInsideHitbox)
                return;

            if (target == null)
                return;

            if (!target.TryGetComponent(out Interactable interactable))
                return;

            interactable.Interacted(this);
        }
    }
}