using UnityEngine;

namespace Gameplay.Shops.UI {
    public class ShopManagerUI : MonoBehaviour {


		[Header("Components")]
		[SerializeField] private ShopPanelUI[] _panels = null;


		private void OnEnable() {
			ShopManager.OnOffersGenerate += SetShopOffers;
		}

		private void OnDisable() {
			ShopManager.OnOffersGenerate -= SetShopOffers;
		}

		private void SetShopOffers(ShopOffer[] offers) {
			for (int i = 0; i < offers.Length; i++) {
				_panels[i].SetOffer(offers[i]);
			}
		}
	}
}