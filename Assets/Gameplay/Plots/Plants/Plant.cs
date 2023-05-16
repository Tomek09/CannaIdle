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
			RefreshShadow();
		}

		public void SetPlant(PlantPreset preset, int growthIndex, float duration) {
			if (preset == null) return;

			_preset = preset;
			RefreshShadow();
			SetPlantData(growthIndex, duration);

			Game.GameManager.OnGameTick += OnGameTick;
		}

		public void SetPlantData(int growthIndex, float duration) {
			Plants.GrowthStage growthStage = _preset.GetGrowthStage(growthIndex);
			_growthIndex = growthIndex;
			_duration = duration;
			_target = growthStage.duration;
			_plantSprite.sprite = growthStage.icon;
			_isFullyGrowth = _preset.IsFullyGrowth(growthIndex);
		}

		public void RemovePlant() {
			_preset = null;
			_plantSprite.sprite = null;
			RefreshShadow();

			Game.GameManager.OnGameTick -= OnGameTick;
		}

		private void RefreshShadow() {
			_shadowSprite.enabled = _preset != null;
		}

		private void OnGameTick() {
			if (_isFullyGrowth) return;

			_duration += 1f;
			if (_duration >= _target) {
				GrowthUp();
			}
		}

		private void GrowthUp() {
			if (_isFullyGrowth) return;
			SetPlantData(_growthIndex + 1, 0f);
		}
	}
}
