using Tools;
using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Shops {
	public class ShopManager : Singleton<ShopManager> {

		private const int TOTAL_OFFERS_PER_DAY = 3;

		[Header("Info")]
		private ShopOffer[] _currentOffers = null;


		public static System.Action<ShopOffer[]> OnOffersGenerate;
		public static System.Action<ShopOffer> OnOfferModify;

		private protected override void Awake() {
			base.Awake();
			_currentOffers = new ShopOffer[TOTAL_OFFERS_PER_DAY];
		}

		private void Start() {
			GenerateShopOffers();
		}

		private void GenerateShopOffers() {
			List<Items.ShopItem> allItems = Items.ItemsManager.instance.GetAllShopItems();
			for (int i = 0; i < TOTAL_OFFERS_PER_DAY; i++) {
				Items.ShopItem shopItem = allItems.GetRandom();
				allItems.Remove(shopItem);

				_currentOffers[i] = new ShopOffer(shopItem, 999);
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

			OnOfferModify.Invoke(shopOffer);

			return true;
		}
	}
}