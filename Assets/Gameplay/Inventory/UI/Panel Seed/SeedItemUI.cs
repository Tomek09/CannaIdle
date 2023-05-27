using System;
using TMPro;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Inventory.UI.Seeds {
    public class SeedItemUI : MonoBehaviour {

		[Header("Buttons")]
		[SerializeField] private Button _button = null;

		[Header("Components")]
		[SerializeField] private Image _background = null;
		[SerializeField] private Image _icon = null;
		[SerializeField] private TMP_Text _name = null;
		[SerializeField] private TMP_Text _quantity = null;

		[Header("Transforms")]
		[SerializeField] private GameObject _containerGO = null;

		[Header("Info")]
		private InventoryItem _currentItem = null;

		private void Start() {
			_button.onClick.AddListener(OnButtonClick);
		}

		public void SetItem(InventoryItem inventoryItem) {
			_currentItem = inventoryItem;
			_containerGO.SetActive(true);

			_icon.sprite = inventoryItem.itemPreset.icon;
			_name.text = inventoryItem.itemPreset.displayName;
			SetQuantity(inventoryItem.quantity);
		}

		public void SetEmpty() {
			_currentItem = null;
			_containerGO.SetActive(false);
		}

		public void SetQuantity(int total) {
			_quantity.text = string.Format("x{0}", total);
		}

		private void OnButtonClick() {
			if (_currentItem == null) return;

			InventoryManager.instance.Equip(_currentItem.itemPreset);
		}

		public void OnItemEquip() {
			SetBackgroundColor(Color.blue);
		}

		public void OnItemUnequip() {
			SetBackgroundColor(Color.white);
		}

		public void SetBackgroundColor(Color color) {
			_background.color = color;
		}

		public InventoryItem GetInventoryItem() => _currentItem;

	}
}