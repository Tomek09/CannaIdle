using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Inventory.UI {
	public class HotbarItemUI : MonoBehaviour {
		[Header("Components")]
		[SerializeField] private Button _button = null;

		[Header("Components")]
		[SerializeField] private Image _background = null;
		[SerializeField] private Image _icon = null;
		[SerializeField] private TMP_Text _quantity = null;

		[Header("Info")]
		private InventoryItem _inventoryItem = null;

		private void Start() {
			_button.onClick.AddListener(OnClick);
		}

		public void Initialize(InventoryItem inventoryItem) {
			_inventoryItem = inventoryItem;

			_icon.sprite = _inventoryItem.itemPreset.icon;
			SetQuantity(_inventoryItem.quantity);
		}

		public void SetQuantity(int quantity) {
			_quantity.text = string.Format("x {0}", quantity);
		}

		public void OnItemEquip() {
			_background.color = Color.blue;
		}

		public void OnItemUnequip() {
			_background.color = Color.white;
		}

		private void OnClick() {
			InventoryManager.instance.Equip(_inventoryItem.itemPreset);
		}
	}
}