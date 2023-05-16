using UnityEngine;

namespace Gameplay.Plots.Plants {
	[CreateAssetMenu(fileName = "Plant Preset - ", menuName = "SO/Plants/Plant Preset")]
	public class PlantPreset : ScriptableObject {

		public string plantCode;
		public GrowthStage[] growthStages = null;

		public GrowthStage GetGrowthStage(int stageIndex) {
			int index = Mathf.Clamp(stageIndex, 0, growthStages.Length - 1);
			return growthStages[index];
		}

		public bool IsFullyGrowth(int stageIndex) => stageIndex >= growthStages.Length - 1;

	}

	[System.Serializable]
	public struct GrowthStage {
		public Sprite icon;
		public float duration;

		public GrowthStage(Sprite icon, float duration) {
			this.icon = icon;
			this.duration = duration;
		}
	}
}