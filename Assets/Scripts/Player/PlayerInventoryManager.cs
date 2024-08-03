using Game.Systems.Items;
using UnityEngine;
namespace Game.Player
{
    using System;

#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(PlayerInventoryManager))]
    public class PlayerInventoryManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var obj = (PlayerInventoryManager) target;
            EditorGUILayout.LabelField("Current Holding Item",
                obj.CurrentHoldingItem.HasValue
                ? Enum.GetName(typeof(ItemTypes), obj.CurrentHoldingItem)
                : "Empty"
                );
            base.OnInspectorGUI();
        }
    }
#endif
    public class PlayerInventoryManager : MonoBehaviour
    {
        public ItemTypes? CurrentHoldingItem { get; private set; }

        [Header("Item Prefabs")]

        [SerializeField] private GameObject masterKeyPrefab;


        private ItemManager currentItemMgr;

        public void HoldItem(ItemTypes item)
        {
            if (CurrentHoldingItem.HasValue) return;
            CurrentHoldingItem = item;

            var obj = item switch
            {
                ItemTypes.MasterKey => masterKeyPrefab,
                _ => null
            };

            if (obj == null)
            {
                Debug.LogError("Item not yet implemented");
                return;
            }


            var itemObj = Instantiate(obj, transform);
            currentItemMgr = itemObj.GetComponent<ItemManager>();
            currentItemMgr.Held();
        }

        public void DropItem()
        {
            CurrentHoldingItem = null;
            currentItemMgr.Dropped();
            currentItemMgr = null;
        }
    }
}