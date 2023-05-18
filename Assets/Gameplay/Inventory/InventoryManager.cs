using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Inventory {
	public class InventoryManager : Singleton<InventoryManager> {

		[Header("Info")]
		private Dictionary<Items.ItemPreset, InventoryItem> _currentItems = null;
		private Items.ItemPreset _equipItem = null;

		[Header("Debug")]
		[SerializeField] private Items.ItemPreset _preset;
		[SerializeField] private int _quantity;

		public static System.Action<Items.ItemPreset> OnItemEquip;
		public static System.Action<Items.ItemPreset> OnItemUnequip;

		public static System.Action<InventoryItem> OnItemAdd;
		public static System.Action<InventoryItem> OnItemQuantityChange;
		public static System.Action<Items.ItemPreset> OnItemRemove;

		private protected override void Awake() {
			base.Awake();
			_currentItems = new Dictionary<Items.ItemPreset, InventoryItem>();
			_equipItem = null;
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.K)) {
				AddItem(_preset, _quantity);
			}
		}

		public void AddItem(Items.ItemPreset itemPreset, int quantity) {
			if (!_currentItems.ContainsKey(itemPreset)) {
				_currentItems.Add(itemPreset, new InventoryItem(itemPreset));
				OnItemAdd?.Invoke(_currentItems[itemPreset]);
			}

			_currentItems[itemPreset].quantity += quantity;
			OnItemQuantityChange?.Invoke(_currentItems[itemPreset]);
		}

		public void RemoveItem(Items.ItemPreset item, int quantity = 1) {
			if (!_currentItems.ContainsKey(item)) return;

			_currentItems[item].quantity -= quantity;
			OnItemQuantityChange?.Invoke(_currentItems[item]);

			if (_currentItems[item].quantity > 0) return;

			if (_equipItem != null && _equipItem == item) {
				Unequip();
			}

			_currentItems.Remove(item);
			OnItemRemove?.Invoke(item);
		}

		public void Equip(Items.ItemPreset itemPreset) {
			if (!_currentItems.ContainsKey(itemPreset)) return;

			if (_equipItem != null && _equipItem == itemPreset) {
				Unequip();
				return;
			}

			Unequip();

			_equipItem = itemPreset;
			_equipItem.OnEquip();
			OnItemEquip?.Invoke(itemPreset);
		}

		public void Unequip() {
			if (_equipItem == null) return;

			_equipItem.OnUnequip();
			OnItemUnequip?.Invoke(_equipItem);
			_equipItem = null;
		}
	}
}