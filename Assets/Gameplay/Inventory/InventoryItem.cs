using UnityEngine;

namespace Gameplay.Inventory {
    public class InventoryItem {

        public Items.ItemPreset itemPreset;
        public int quantity;

        public InventoryItem(Items.ItemPreset itemPreset) {
            this.itemPreset = itemPreset;
            this.quantity = 0;
        }
	}
}