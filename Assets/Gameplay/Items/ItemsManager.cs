using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items {
	public class ItemsManager : Singleton<ItemsManager> {

		[Header("Components")]
		[SerializeField] private Tools.Logger _errorLogger = null;

		[Header("Info")]
		private Dictionary<string, ItemPreset> _presets = null;
		private List<ItemPreset> _allPresets = null;

		private List<ShopItem> _allshopItems = null;

		private protected override void Awake() {
			base.Awake();

			_presets = new Dictionary<string, ItemPreset>();
			_allPresets = new List<ItemPreset>();
			ItemPreset[] presets = Resources.LoadAll<ItemPreset>("Items");
			foreach (ItemPreset preset in presets) {
				if (_presets.ContainsKey(preset.itemCode)) {
					_errorLogger.Log($"Duplicated preset code: {preset.itemCode}");
					continue;
				}

				_presets.Add(preset.itemCode, preset);
				_allPresets.Add(preset);
			}

			ShopItem[] shopItems = Resources.LoadAll<ShopItem>("Shop Items");
			_allshopItems = new List<ShopItem>();
			_allshopItems.AddRange(shopItems);
		}

		public ItemPreset GetItem(string itemCode) {
			if (_presets.ContainsKey(itemCode)) {
				return _presets[itemCode];
			} else {
				_errorLogger.Log($"Missing item preset: {itemCode}");
				return null;
			}
		}

		public bool TryGetItem(string itemCode, out ItemPreset item) {
			item = null;

			if (_presets.ContainsKey(itemCode)) {
				item = _presets[itemCode];
			} else {
				_errorLogger.Log($"Missing item preset: {itemCode}");
			}

			return item != null;
		}

		public List<ItemPreset> GetAllItems() => _allPresets;

		public List<ItemPreset> GetAllItems(ItemCategory itemCategory) {
			List<ItemPreset> items = new List<ItemPreset>();
			for (int i = 0; i < _allPresets.Count; i++) {
				if (Equals(_allPresets[i].itemCategory, itemCategory)) {
					items.Add(_allPresets[i]);
				}
			}
			return items;
		}


		public List<ShopItem> GetAllShopItems() => _allshopItems;

	}
}