using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Shops.UI {
	public class ShopManagerUI : MonoBehaviour {

		private const int MAX_OFFERS_PER_PAGE = 3;

		[Header("Components")]
		[SerializeField] private ShopPanelUI[] _panels = null;
		private List<ShopOffer> _currentOffers = null;

		private void OnEnable() {
			ShopManager.OnOffersGenerate += OnOffersGenerate;
		}

		private void OnDisable() {
			ShopManager.OnOffersGenerate -= OnOffersGenerate;
		}

		private void Start() {
			for (int i = 0; i < _panels.Length; i++) {
				_panels[i].Initialize();
			}
		}

		public void OpenPage(int page) {
			int startIndex = page * MAX_OFFERS_PER_PAGE;
			for (int i = 0; i < MAX_OFFERS_PER_PAGE; i++) {
				if (startIndex + i < 0 || startIndex + i >= _currentOffers.Count) {
					_panels[i].gameObject.SetActive(false);
					continue;
				}
				_panels[i].gameObject.SetActive(true);
				_panels[i].SetOffer(_currentOffers[startIndex + i]);
			}
		}


		private void OnOffersGenerate(List<ShopOffer> shopOffers) {
			_currentOffers = shopOffers;
		}

	}
}