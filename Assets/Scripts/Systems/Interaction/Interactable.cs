using Game.Player;
using UnityEngine;

namespace Game.Systems.Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        protected const int INTERACTABLE_LAYER = 6;
        public virtual void Hovered(PlayerInteractor interactor) { }
        public virtual void Unhovered(PlayerInteractor interactor) { }
        public virtual void Interacted(PlayerInteractor interactor) { }
    }
}