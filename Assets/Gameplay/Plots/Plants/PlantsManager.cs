using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Plots.Plants {
	public class PlantsManager : Singleton<PlantsManager> {

		[Header("Components")]
		[SerializeField] private Tools.Logger _errorLogger = null;

		[Header("Info")]
		private Dictionary<string, PlantPreset> _presets = null;

		private protected override void Awake() {
			base.Awake();

			_presets = new Dictionary<string, PlantPreset>();
			PlantPreset[] presets = Resources.LoadAll<PlantPreset>("Plants");
			foreach (PlantPreset preset in presets) {
				if (_presets.ContainsKey(preset.plantCode)) {
					_errorLogger.Log($"Duplicated preset code: {preset.plantCode}");
					continue;
				}
				_presets.Add(preset.plantCode, preset);
			}
		}

		public bool TryGetPlant(string plantCode, out PlantPreset plant) {
			plant = null;

			if(_presets.ContainsKey(plantCode)) {
				plant = _presets[plantCode];
			} else {
				_errorLogger.Log($"Missing plant preset: {plantCode}");
			}

			return plant != null;
		}
	}
}