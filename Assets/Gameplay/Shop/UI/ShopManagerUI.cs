using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Shops.UI {
	public class ShopManagerUI : MonoBehaviour {

		private const int MAX_OFFERS_PER_PAGE = 3;

		[Header("Components")]
		[SerializeField] private ShopPanelUI[] _panels = null;
		private List<ShopOffer> _currentOffers = null;

		[Header("Navigation")]
		[SerializeField] private Button _nextPageButton;
		[SerializeField] private Button _previousPageButton;
		private int _currentPage = 0;
		private int _maxPage = 0;

		private void OnEnable() {
			ShopManager.OnOffersGenerate += OnOffersGenerate;
		}

		private void OnDisable() {
			ShopManager.OnOffersGenerate -= OnOffersGenerate;
		}

		public void Initialize() {
			for (int i = 0; i < _panels.Length; i++) {
				_panels[i].Initialize();
			}

			_nextPageButton.onClick.AddListener(OnNextPage);
			_previousPageButton.onClick.AddListener(OnPreviousPage);
		}

		private void OnNextPage() {
			SetPage(1);
		}

		private void OnPreviousPage() {
			SetPage(-1); 
		}


		private void SetPage(int direction) {
			_currentPage = Mathf.Clamp(_currentPage + direction, 0, _maxPage);
			OpenPage(_currentPage);
		}

		private void OpenPage(int page) {
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
			_maxPage = Mathf.RoundToInt(_currentOffers.Count / MAX_OFFERS_PER_PAGE);
			OpenPage(0);
		}
	}
}