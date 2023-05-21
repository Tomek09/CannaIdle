using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Gameplay.Shops {
	public class ShopManager : Singleton<ShopManager> {

		private const int TOTAL_OFFERS_PER_DAY = 3;

		[Header("Info")]
		private ShopOffer[] _currentOffers = null;


		public static System.Action<ShopOffer[]> OnOffersGenerate;

		private protected override void Awake() {
			base.Awake();
			_currentOffers = new ShopOffer[TOTAL_OFFERS_PER_DAY];
		}


		private void GenerateShopOffers() {
			List<Items.ItemPreset> allItems = Items.ItemsManager.instance.GetAllItems();
			for (int i = 0; i < TOTAL_OFFERS_PER_DAY; i++) {
				Items.ItemPreset itemPreset = allItems.GetRandom();
				allItems.Remove(itemPreset);

				_currentOffers[i] = new ShopOffer(itemPreset);
			}

			OnOffersGenerate?.Invoke(_currentOffers);
		}
	}
}