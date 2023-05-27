using UnityEngine;

namespace Gameplay.Shops {
    [System.Serializable]
    public class ShopOffer {
        public Items.ShopItem shopItem;
        public int quantity;

        public ShopOffer(Items.ShopItem shopItem, int quantity) {
            this.shopItem = shopItem;
            this.quantity = quantity;
        }
    }
}