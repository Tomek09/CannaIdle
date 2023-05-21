using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Inventory {
	public class InventoryManager : Singleton<InventoryManager> {

		[Header("Info")]
		private Dictionary<Items.ItemPreset, InventoryItem> _currentItems = null;
		private Items.ItemPreset _equipItem = null;
		private int _totalCoins = 0;

		public static System.Action<Items.ItemPreset> OnItemEquip;
		public static System.Action<Items.ItemPreset> OnItemUnequip;

		public static System.Action<InventoryItem> OnItemAdd;
		public static System.Action<InventoryItem> OnItemQuantityChange;
		public static System.Action<Items.ItemPreset> OnItemRemove;


		private void OnEnable() {
			Game.Save.SaveManager.OnGameSave += OnGameSave;
			Game.Save.SaveManager.OnGameLoad += OnGameLoad;
		}

		private void OnDisable() {
			Game.Save.SaveManager.OnGameSave -= OnGameSave;
			Game.Save.SaveManager.OnGameLoad -= OnGameLoad;
		}

		private protected override void Awake() {
			base.Awake();
			_currentItems = new Dictionary<Items.ItemPreset, InventoryItem>();
			_equipItem = null;
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
			if (!_currentItems.ContainsKey(item)) {
				return;
			}

			_currentItems[item].quantity -= quantity;
			OnItemQuantityChange?.Invoke(_currentItems[item]);

			if (_currentItems[item].quantity > 0) {
				return;
			}

			if (_equipItem != null && _equipItem == item) {
				Unequip();
			}

			_currentItems.Remove(item);
			OnItemRemove?.Invoke(item);
		}

		public void Equip(Items.ItemPreset itemPreset) {
			if (!_currentItems.ContainsKey(itemPreset)) {
				return;
			}

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
			if (_equipItem == null) {
				return;
			}

			_equipItem.OnUnequip();
			OnItemUnequip?.Invoke(_equipItem);
			_equipItem = null;
		}


		public void AddCoins(int amount) {
			ModifyCoins(_totalCoins + Mathf.Abs(amount));
		}

		public void RemoveCoins(int amount) {
			ModifyCoins(_totalCoins - Mathf.Abs(amount));
		}

		private void ModifyCoins(int newAmount) {
			_totalCoins = Mathf.Clamp(newAmount, 0, int.MaxValue);
		}


		public bool ContainsCoins(int total) {
			return total >= _totalCoins;
		}

		#region Save/Load

		private void OnGameSave(Game.Save.GameData gameData) {
			Game.Save.SaveInventory saveInventory = new Game.Save.SaveInventory() {
				coins = 0,
				inventoryItems = new Game.Save.SaveItem[_currentItems.Keys.Count]
			};

			int index = 0;
			foreach (KeyValuePair<Items.ItemPreset, InventoryItem> item in _currentItems) {
				saveInventory.inventoryItems[index] = new Game.Save.SaveItem(item.Value.itemPreset.itemCode, item.Value.quantity);
				index++;
			}

			gameData.inventory = saveInventory;
		}

		private void OnGameLoad(Game.Save.GameData gameData) {
			foreach (Game.Save.SaveItem item in gameData.inventory.inventoryItems) {
				if (Items.ItemsManager.instance.TryGetItem(item.itemCode, out Items.ItemPreset itemPreset)) {
					AddItem(itemPreset, item.quantity);
				}
			}
		}

		#endregion
	}
}