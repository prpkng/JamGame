using Game.Player;
using UnityEngine;

namespace Game.Systems.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        public virtual void Hovered(PlayerInteractor interactor) { }
        public virtual void Unhovered(PlayerInteractor interactor) { }
        public virtual void Interacted(PlayerInteractor interactor) { }
    }
}