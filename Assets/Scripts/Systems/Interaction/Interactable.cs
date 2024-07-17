using Game.Player;
using UnityEngine;

namespace Game.Systems.Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        public virtual void Hovered(PlayerInteractor interactor) { }
        public virtual void Unhovered(PlayerInteractor interactor) { }
        public virtual void Interacted(PlayerInteractor interactor) { }
    }
}