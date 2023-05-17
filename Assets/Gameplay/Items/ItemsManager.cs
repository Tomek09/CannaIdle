using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items {
	public class ItemsManager : Singleton<ItemsManager> {

		[Header("Components")]
		[SerializeField] private Tools.Logger _errorLogger = null;

		[Header("Info")]
		private Dictionary<string, ItemPreset> _presets = null;

		private protected override void Awake() {
			base.Awake();

			_presets = new Dictionary<string, ItemPreset>();
			ItemPreset[] presets = Resources.LoadAll<ItemPreset>("Items");
			foreach (ItemPreset preset in presets) {
				if (_presets.ContainsKey(preset.itemCode)) {
					_errorLogger.Log($"Duplicated preset code: {preset.itemCode}");
					continue;
				}
				_presets.Add(preset.itemCode, preset);
			}
		}
	}
}