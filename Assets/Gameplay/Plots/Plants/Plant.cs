using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace Gameplay.Plots.Plants {
	public class Plant : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private SpriteRenderer _shadowSprite = null;
		[SerializeField] private SpriteRenderer _plantSprite = null;

		[Header("Info")]
		private Vector3 _plantPosition;
		private Vector3 _plantSize;
		private PlantPreset _preset = null;
		private int _growthIndex;
		private float _growthDuration;
		private float _growthTarget;
		private bool _isFullyGrowth;
		private bool _durningAnimation;

		public void Initialize() {
			_preset = null;

			_plantSprite.sprite = null;
			_shadowSprite.sprite = null;

			_plantPosition = _plantSprite.transform.localPosition;
			_plantSize = _plantSprite.transform.localScale;
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
			_durningAnimation = true;
			_preset = null;
			_shadowSprite.sprite = null;
			Game.GameManager.OnGameTick -= OnGameTick;

			OnHarvestEffect(() => {
				_durningAnimation = false;
				_plantSprite.sprite = null;
				_plantSprite.transform.localPosition = Vector3.zero;
			});
		}


		public void SetPlantData(int growthIndex, float duration) {
			GrowthStage growthStage = _preset.GetGrowthStage(growthIndex);
			_growthIndex = growthIndex;
			_growthDuration = duration;
			_growthTarget = growthStage.duration;
			_isFullyGrowth = _preset.IsFullyGrowth(growthIndex);

			Sprite icon = growthStage.icon;

			_plantSprite.sprite = icon;
			_shadowSprite.sprite = icon;
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
			OnGrowthEffect();
		}


		private void OnGrowthEffect() {
			Transform plantTransform = _plantSprite.transform;
			plantTransform.DOKill();
			plantTransform.transform.localScale = _plantSize;

			Vector3 initScale = _plantSprite.transform.localScale;
			Vector3 offset = Vector3.up * .25f;
			Vector3 targetUp = initScale + offset;
			Vector3 targetDown = initScale - offset;
			float duration = .125f;

			Sequence mySequence = DOTween.Sequence();
			mySequence.Append(plantTransform.DOScale(targetUp, duration).SetEase(Ease.OutBack));
			mySequence.Append(plantTransform.DOScale(targetDown, duration).SetEase(Ease.Linear));
			mySequence.Append(plantTransform.DOScale(initScale, duration).SetEase(Ease.InBack));
		}

		private void OnHarvestEffect(System.Action onComplete) {
			Transform plantTransform = _plantSprite.transform;
			plantTransform.DOKill();
			plantTransform.transform.localPosition = _plantPosition;

			Vector3 initPosition = _plantSprite.transform.localPosition;
			float distance = .5f;
			float duration = .125f;

			plantTransform.DOKill();
			plantTransform.DOMoveY(initPosition.y + distance, duration)
				.SetEase(Ease.OutBack)
				.OnComplete(() => onComplete?.Invoke());
		}

		public bool CanPlant() {
			return _preset == null && !_durningAnimation;
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
