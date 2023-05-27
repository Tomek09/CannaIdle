using UnityEngine;

namespace Gameplay.Plots.Plants {
	public class Plant : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private SpriteRenderer _shadowSprite = null;
		[SerializeField] private SpriteRenderer _plantSprite = null;

		[Header("Info")]
		private PlotTile _tile = null;
		private PlantPreset _preset = null;
		private int _growthIndex;
		private float _growthDuration;
		private float _growthTarget;
		private bool _isFullyGrowth;

		public void Initialize(PlotTile tile) {
			_tile = tile;
			_preset = null;

			_plantSprite.sprite = null;
			_shadowSprite.sprite = null;
		}


		public void SetPlant(PlantPreset preset) {
			SetPlant(preset, 0, 0);
		}

		public void SetPlant(PlantPreset preset, int growthIndex, float duration) {
			if (preset == null) return;

			_preset = preset;
			SetPlantData(growthIndex, duration);

			Game.GameManager.OnGameTick += OnGameTick;
		}

		public void Collect() {
			Inventory.InventoryManager.instance.AddCoins(_preset.collectCoin);
		}

		public void RemovePlant() {
			_preset = null;
			_plantSprite.sprite = null;
			_shadowSprite.sprite = null;

			Game.GameManager.OnGameTick -= OnGameTick;
		}


		public void SetPlantData(int growthIndex, float duration) {
			GrowthStage growthStage = _preset.GetGrowthStage(growthIndex);
			_growthIndex = growthIndex;
			_growthDuration = duration;
			_growthTarget = growthStage.duration;
			_isFullyGrowth = _preset.IsFullyGrowth(growthIndex);

			_plantSprite.sprite = growthStage.icon;
			_shadowSprite.sprite = growthStage.icon;
		}

		private void OnGameTick() {
			if (_isFullyGrowth) return;
			AddGrowthDuration(Time.deltaTime);
		}

		public void AddGrowthDuration(float value) {
			_growthDuration += value;
			if (_growthDuration >= _growthTarget) {
				GrowthUp();
			}
		}

		private void GrowthUp() {
			if (_isFullyGrowth) return;
			SetPlantData(_growthIndex + 1, 0f);
		}


		public PlantPreset GetPlantPreset() => _preset;

		public bool IsFullyGrowth() => _isFullyGrowth;

		#region Save/Load

		public Game.Save.SavePlotPlant Save() {
			Game.Save.SavePlotPlant savePlant = new Game.Save.SavePlotPlant();

			if (_preset != null) {
				savePlant = new Game.Save.SavePlotPlant(_preset.plantCode, _growthIndex, _growthDuration);
			}

			return savePlant;
		}

		public void Load(Game.Save.SavePlotPlant plant) {
			if (string.IsNullOrEmpty(plant.plantCode)) return;

			if (!PlantsManager.instance.TryGetPlant(plant.plantCode, out PlantPreset plantPreset)) return;
			SetPlant(plantPreset, plant.growthIndex, plant.growthDuration);
		}

		#endregion
	}
}
