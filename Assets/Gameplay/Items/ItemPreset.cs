using UnityEngine;

namespace Gameplay.Items {
    public abstract class ItemPreset : ScriptableObject {

        [Header("Core Settings")]
        public string itemCode = "item_";
        public Sprite icon;
        public ItemCategory itemCategory;

		public abstract void OnEquip();
        public abstract void OnUnequip();
    }
}