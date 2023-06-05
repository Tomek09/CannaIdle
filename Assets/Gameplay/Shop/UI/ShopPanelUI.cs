using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

namespace Gameplay.Shops.UI {
	public class ShopPanelUI : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private RectTransform _rectPanel = null;
		[SerializeField] private Image _icon = null;
		[SerializeField] private TMP_Text _name = null;
		[SerializeField] private TMP_Text _priceText = null;
		//[SerializeField] private TMP_Text _quantity = null;

		[Header("Buttons")]
		[SerializeField] private Button _buyButton = null;

		[Header("Info")]
		private ShopOffer _offer = null;

		public void Initialize() {
			_buyButton.onClick.AddListener(OnBuyButton);
		}

		public void SetOffer(ShopOffer offer) {
			_offer = offer;
			RefreshUI();
		}

		private void OnBuyButton() {
			if (!ShopManager.instance.TryBuyOffer(_offer)) {
				return;
			}

			_rectPanel.DOKill();
			_rectPanel.localScale = Vector3.one * 1.1f;
			_rectPanel.DOScale(Vector3.one, .125f);
		}

		public void RefreshUI() {
			_icon.sprite = _offer.shopItem.item.icon;
			_name.text = _offer.shopItem.item.displayName;
			_priceText.text = string.Format("<sprite=0> {0}", _offer.shopItem.cost);
			//_quantity.text = string.Format("x{0}", _offer.quantity);
		}

	}
}