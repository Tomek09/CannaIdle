using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Inventory.UI.Seeds {
	public class PanelSeedsUI : MonoBehaviour {

		private const int TOTAL_ITEMS_PER_TAB = 4;

		[Header("Components")]
		[SerializeField] private RectTransform _rectTransform = null;

		[Header("Buttons")]
		[SerializeField] private Button _hideButton = null;
		[SerializeField] private Button _leftNavArrow;
		[SerializeField] private Button _rightNavArrow;

		[Header("Values")]
		[SerializeField] private SeedItemUI[] _currentItemUI = null;

		[Header("Info")]
		private bool _isHidden = true;
		private int _currentTab = 0;
		private int _totalTabs = 1;
		private List<InventoryItem> _items = null;
		private Items.ItemPreset _currentEquip = null;


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
			_items = new List<InventoryItem>();

			_hideButton.onClick.AddListener(OnHideButton);
			_leftNavArrow.onClick.AddListener(() => OnChangeNavigation(-1));
			_rightNavArrow.onClick.AddListener(() => OnChangeNavigation(1));
		}

		private void OnHideButton() {
			ChangeHide();
		}

		private void ChangeHide() {
			_isHidden = !_isHidden;
			float desination = _isHidden ? 100f : 500f;

			_rectTransform.DOKill();
			_rectTransform.DOAnchorPosX(desination, .25f)
				.SetEase(Ease.OutBack);
		}

		private void OnChangeNavigation(int dir) {
			_currentTab = Mathf.Clamp(_currentTab + dir, 0, _totalTabs);
			RefreshTab();
		}

		private void RefreshTab() {
			int startIndex = _currentTab * TOTAL_ITEMS_PER_TAB;
			for (int i = 0; i < TOTAL_ITEMS_PER_TAB; i++) {
				int itemIndex = startIndex + i;
				if (!ContainItem(itemIndex)) {
					_currentItemUI[i].SetEmpty();
				} else {
					_currentItemUI[i].SetItem(_items[itemIndex]);

					_currentItemUI[i].OnItemUnequip();
					if (_currentEquip != null && Equals(_items[itemIndex].itemPreset, _currentEquip)) {
						_currentItemUI[i].OnItemEquip();
					}

				}
			}
		}

		#region Callbacks

		private void OnItemEquip(Items.ItemPreset itemPreset) {
			if (itemPreset.itemCategory != Items.ItemCategory.Seeds) return;
			_currentEquip = itemPreset;
			RefreshTab();
		}

		private void OnItemUnequip(Items.ItemPreset itemPreset) {
			if (itemPreset.itemCategory != Items.ItemCategory.Seeds) return;
			_currentEquip = null;
			RefreshTab();
		}


		private void OnItemAdd(InventoryItem inventoryItem) {
			if (inventoryItem.itemPreset.itemCategory != Items.ItemCategory.Seeds) return;

			_items.Add(inventoryItem);
			RefreshTab();
		}

		private void OnItemQuantityChange(InventoryItem inventoryItem) {
			if (inventoryItem.itemPreset.itemCategory != Items.ItemCategory.Seeds) return;

			RefreshTab();
		}

		private void OnItemRemove(Items.ItemPreset itemPreset) {
			if (itemPreset.itemCategory != Items.ItemCategory.Seeds) return;

			InventoryItem inventoryItem = FindInventoryItem(itemPreset);
			_items.Remove(inventoryItem);
			RefreshTab();
		}

		#endregion

		private bool ContainItem(int index) {
			return index >= 0 && index < _items.Count;
		}

		private InventoryItem FindInventoryItem(Items.ItemPreset itemPreset) {
			for (int i = 0; i < _items.Count; i++) {
				if (Equals(_items[i].itemPreset, itemPreset))
					return _items[i];
			}
			return null;
		}
	}
}