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
			List<Items.ItemPreset> allItems = Items.ItemsManager.instance.GetAllItems(Items.ItemCategory.Seeds);
			for (int i = 0; i < TOTAL_OFFERS_PER_DAY; i++) {
				Items.ItemPreset itemPreset = allItems.GetRandom();
				allItems.Remove(itemPreset);

				_currentOffers[i] = new ShopOffer(itemPreset);
			}

			OnOffersGenerate?.Invoke(_currentOffers);
		}

		public bool TryBuyOffer(ShopOffer shopOffer) {
			int cost = 0;
			if (!Inventory.InventoryManager.instance.ContainsCoins(cost)) {
				return false;
			}

			Inventory.InventoryManager.instance.RemoveCoins(cost);
			Inventory.InventoryManager.instance.AddItem(shopOffer.itemPreset, 1);
			shopOffer.quantity = Mathf.Clamp(shopOffer.quantity - 1, 0, int.MaxValue);

			OnOfferModify.Invoke(shopOffer);

			return true;
		}
	}
}