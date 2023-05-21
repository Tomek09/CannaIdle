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
		private int _maxSeedsTabs = 0;
		private Items.ItemPreset[] _items = null;
		private Dictionary<Items.ItemPreset, InventoryItem> _seedsItems = null;

		private Items.ItemPreset _currentEquip = null;


		private void OnEnable() {
			InventoryManager.OnItemEquip += OnItemEquip;
			InventoryManager.OnItemUnequip += OnItemUnequip;

			InventoryManager.OnItemQuantityChange += OnItemQuantityChange;
		}

		private void OnDisable() {
			InventoryManager.OnItemEquip -= OnItemEquip;
			InventoryManager.OnItemUnequip -= OnItemUnequip;

			InventoryManager.OnItemQuantityChange -= OnItemQuantityChange;
		}

		private void Awake() {
			_hideButton.onClick.AddListener(OnHideButton);
			_leftNavArrow.onClick.AddListener(() => OnChangeNavigation(-1));
			_rightNavArrow.onClick.AddListener(() => OnChangeNavigation(1));

			InitializeUI();
		}

		private void OnHideButton() {
			ChangeHide();
		}

		private void OnChangeNavigation(int dir) {
			_currentTab = Mathf.Clamp(_currentTab + dir, 0, _maxSeedsTabs);
			RefreshTab();
		}

		#region UI

		private void InitializeUI() {
			List<Items.ItemPreset> seedsItems = Items.ItemsManager.instance.GetAllItems(Items.ItemCategory.Seeds);
			_items = new Items.ItemPreset[seedsItems.Count];
			_seedsItems = new Dictionary<Items.ItemPreset, InventoryItem>();

			for (int i = 0; i < seedsItems.Count; i++) {
				Items.ItemPreset itemPreset = seedsItems[i];
				_items[i] = itemPreset;
				_seedsItems.Add(itemPreset, new InventoryItem(itemPreset));
			}

			float totalSeeds = seedsItems.Count / (float)TOTAL_ITEMS_PER_TAB;
			_maxSeedsTabs = Mathf.FloorToInt(totalSeeds);
		}

		private void ChangeHide() {
			_isHidden = !_isHidden;
			float desination = _isHidden ? 100f : 500f;

			_rectTransform.DOKill();
			_rectTransform.DOAnchorPosX(desination, .25f)
				.SetEase(Ease.OutBack);
		}

		private void RefreshTab() {
			int startIndex = _currentTab * TOTAL_ITEMS_PER_TAB;
			for (int i = 0; i < TOTAL_ITEMS_PER_TAB; i++) {
				int itemIndex = startIndex + i;
				int slotIndex = i;

				if(itemIndex >= _items.Length) {
					_currentItemUI[slotIndex].SetEmpty();
					continue;
				}

				_currentItemUI[slotIndex].SetItem(_seedsItems[_items[itemIndex]]);
				_currentItemUI[slotIndex].OnItemUnequip();

				if(_currentEquip != null && Equals(_currentEquip, _items[itemIndex])) {
					_currentItemUI[slotIndex].OnItemEquip();
				}
			}
		}

		#endregion

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


		private void OnItemQuantityChange(InventoryItem inventoryItem) {
			if (inventoryItem.itemPreset.itemCategory != Items.ItemCategory.Seeds) return;

			_seedsItems[inventoryItem.itemPreset].quantity = inventoryItem.quantity;

			RefreshTab();
		}

		#endregion
	}
}