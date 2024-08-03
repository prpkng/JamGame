using System;
using Game.Player;
using Game.Systems.Items;
using UnityEngine;

namespace Game.Systems.Interaction
{
    public class CollectableItemInteractable : Interactable
    {
        [SerializeField] private ItemTypes itemType;
        public override void Hovered(PlayerInteractor interactor)
        { }

        public override void Interacted(PlayerInteractor interactor)
        {
            print($"Collected Item: {Enum.GetName(typeof(ItemTypes), itemType)}");
            PlayerManager.LocalPlayer.inventory.HoldItem(itemType);
            Destroy(transform.parent.gameObject);
        }

        public override void Unhovered(PlayerInteractor interactor)
        { }

    }
}