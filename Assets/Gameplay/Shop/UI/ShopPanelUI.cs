using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Shops.UI {
	public class ShopPanelUI : MonoBehaviour {

		[Header("Buttons")]
		[SerializeField] private Button _buyButton = null;

		[Header("Components")]
		[SerializeField] private Image _icon = null;
		[SerializeField] private TMP_Text _name = null;
		[SerializeField] private TMP_Text _quantity = null;
		[SerializeField] private TMP_Text _buyText = null;

		[Header("Info")]
		private ShopOffer _offer = null;
		private int _cost = 0;

		public void Initialize() {
			_buyButton.onClick.AddListener(OnBuyButton);
		}

		public void SetOffer(ShopOffer offer) {
			_offer = offer;
			_cost = 0;
			RefreshUI();
		}

		private void OnBuyButton() {
			if (!Inventory.InventoryManager.instance.ContainsCoins(_cost)) {
				return;
			}

			Inventory.InventoryManager.instance.AddItem(_offer.itemPreset, 1);
			Inventory.InventoryManager.instance.RemoveCoins(_cost);
		}

		private void RefreshUI() {
			_icon.sprite = _offer.itemPreset.icon;
			_name.text = _offer.itemPreset.itemCode;
			_quantity.text = string.Format("x{0}", _offer.quantity);

			_buyText.text = string.Format("<sprite=0> {0}", _cost);
		}

	}
}