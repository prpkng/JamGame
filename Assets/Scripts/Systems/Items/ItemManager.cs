using UnityEngine;

namespace Game.Systems.Items
{
    public abstract class ItemManager : MonoBehaviour
    {
        public abstract void Held();
        public abstract void Dropped();
    }
}