using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Inventory.UI.Hotbar {
	public class HotbarUI : MonoBehaviour {

		[Header("Prefabs")]
		[SerializeField] private HotbarItemUI _hotbarPrefab = null;
		[SerializeField] private Transform _parent = null;

		[Header("Info")]
		private Dictionary<Items.ItemPreset, HotbarItemUI> _hotbarItems = null;

		private void OnEnable() {
			InventoryManager.OnItemEquip += OnItemEquip;
			InventoryManager.OnItemUnequip += OnItemUnequip;

			InventoryManager.OnItemAdd += OnItemAdd;
			InventoryManager.OnItemQuantityChange += OnItemQuantityChange;
			InventoryManager.OnItemRemove += OnItemRemove;
		}

		private void OnDisable() {
			InventoryManager.OnItemEquip -= OnItemEquip;
			InventoryManager.OnItemUnequip -= OnItemUnequip;

			InventoryManager.OnItemAdd -= OnItemAdd;
			InventoryManager.OnItemQuantityChange -= OnItemQuantityChange;
			InventoryManager.OnItemRemove -= OnItemRemove;
		}

		private void Awake() {
			_hotbarItems = new Dictionary<Items.ItemPreset, HotbarItemUI>();
		}

		#region Callbacks

		private void OnItemEquip(Items.ItemPreset itemPreset) {
			if (itemPreset.itemCategory != Items.ItemCategory.Tools) return;
			_hotbarItems[itemPreset].OnItemEquip();
		}

		private void OnItemUnequip(Items.ItemPreset itemPreset) {
			if (itemPreset.itemCategory != Items.ItemCategory.Tools) return;
			_hotbarItems[itemPreset].OnItemUnequip();
		}


		private void OnItemAdd(InventoryItem inventoryItem) {
			if (inventoryItem.itemPreset.itemCategory != Items.ItemCategory.Tools) return;

			HotbarItemUI hotbarItem = CreateHotbar(inventoryItem);
			_hotbarItems.Add(inventoryItem.itemPreset, hotbarItem);

		}

		private void OnItemQuantityChange(InventoryItem inventoryItem) {
			if (inventoryItem.itemPreset.itemCategory != Items.ItemCategory.Tools) return;

			_hotbarItems[inventoryItem.itemPreset].SetQuantity(inventoryItem.quantity);
		}

		private void OnItemRemove(Items.ItemPreset itemPreset) {
			if (itemPreset.itemCategory != Items.ItemCategory.Tools) return;

			Destroy(_hotbarItems[itemPreset].gameObject);
			_hotbarItems.Remove(itemPreset);
		}

		#endregion

		private HotbarItemUI CreateHotbar(InventoryItem inventoryItem) {
			HotbarItemUI hotbarItem = GameObjects.GOInstantiate(_hotbarPrefab, _parent);
			hotbarItem.Initialize(inventoryItem);
			return hotbarItem;
		}
	}
}