using UnityEngine;

namespace Gameplay.Shops {
    [System.Serializable]
    public class ShopOffer {
        public Items.ItemPreset itemPreset;
        public int quantity;

        public ShopOffer(Items.ItemPreset itemPreset) {
            this.itemPreset = itemPreset;
            quantity = 999;
        }
    }
}