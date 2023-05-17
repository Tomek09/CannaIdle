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
		private float _duration;
		private float _target;
		private bool _isFullyGrowth;

		public void Initialize(PlotTile tile) {
			_tile = tile;
			_preset = null;

			_plantSprite.sprite = null;
			_shadowSprite.sprite = null;
		}


		public void SetPlant(PlantPreset preset, int growthIndex, float duration) {
			if (preset == null) return;

			_preset = preset;
			SetPlantData(growthIndex, duration);

			Game.GameManager.OnGameTick += OnGameTick;
		}

		public void SetPlantData(int growthIndex, float duration) {
			GrowthStage growthStage = _preset.GetGrowthStage(growthIndex);
			_growthIndex = growthIndex;
			_duration = duration;
			_target = growthStage.duration;
			_isFullyGrowth = _preset.IsFullyGrowth(growthIndex);

			_plantSprite.sprite = growthStage.icon;
			_shadowSprite.sprite = growthStage.icon;
		}

		public void RemovePlant() {
			_preset = null;
			_plantSprite.sprite = null;
			_shadowSprite.sprite = null;

			Game.GameManager.OnGameTick -= OnGameTick;
		}


		private void OnGameTick() {
			if (_isFullyGrowth) return;

			_duration += Time.deltaTime;
			if (_duration >= _target) {
				GrowthUp();
			}
		}

		private void GrowthUp() {
			if (_isFullyGrowth) return;
			SetPlantData(_growthIndex + 1, 0f);
		}



		public Game.Save.SavePlotPlant Save() {
			Debug.Log("TODO");
			return new Game.Save.SavePlotPlant();
		}

		public void Load(Game.Save.SavePlotPlant plant) {
			Debug.Log("TODO");
		}

		public PlantPreset GetPlantPreset() => _preset;
	}
}
