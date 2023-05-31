using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Shops {
	public class ShopManager : Singleton<ShopManager> {

		[Header("Info")]
		private List<ShopOffer> _currentOffers = null;
		private List<Items.ShopItem> _allShopItems = null;

		public static System.Action<List<ShopOffer>> OnOffersGenerate;

		private protected override void Awake() {
			base.Awake();
			_allShopItems = Items.ItemsManager.instance.GetAllShopItems();
		}

		private void Start() {
			GenerateOffers();
		}

		public void GenerateOffers() {
			_currentOffers = new List<ShopOffer>(_allShopItems.Count);
			for (int i = 0; i < _allShopItems.Count; i++) {
				int offertQuantity = Random.Range(10, 15);
				_currentOffers.Add(new ShopOffer(_allShopItems[i], offertQuantity));
			}

			OnOffersGenerate?.Invoke(_currentOffers);
		} 


		public bool TryBuyOffer(ShopOffer shopOffer) {
			if (!Inventory.InventoryManager.instance.ContainsCoins(shopOffer.shopItem.cost)) {
				return false;
			}

			Inventory.InventoryManager.instance.RemoveCoins(shopOffer.shopItem.cost);
			Inventory.InventoryManager.instance.AddItem(shopOffer.shopItem.item, 1);
			shopOffer.quantity = Mathf.Clamp(shopOffer.quantity - 1, 0, int.MaxValue);

			return true;
		}
	}
}