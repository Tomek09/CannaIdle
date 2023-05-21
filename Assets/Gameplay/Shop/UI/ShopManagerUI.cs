using UnityEngine;

namespace Gameplay.Shops.UI {
    public class ShopManagerUI : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private ShopPanelUI[] _panels = null;

		private void OnEnable() {
			ShopManager.OnOffersGenerate += SetShopOffers;
			ShopManager.OnOfferModify += RefreshOffer;
		}

		private void OnDisable() {
			ShopManager.OnOffersGenerate -= SetShopOffers;
			ShopManager.OnOfferModify -= RefreshOffer;
		}

		private void Start() {
			for (int i = 0; i < _panels.Length; i++) {
				_panels[i].Initialize();
			}
		}

		private void SetShopOffers(ShopOffer[] offers) {
			for (int i = 0; i < offers.Length; i++) {
				_panels[i].SetOffer(offers[i]);
			}
		}

		private void RefreshOffer(ShopOffer offer) {
			for (int i = 0; i < _panels.Length; i++) {
				_panels[i].RefreshUI();
			}
		}
	}
}