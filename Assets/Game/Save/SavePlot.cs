using UnityEngine;

namespace Game.Save {

	[System.Serializable]
	public class SavePlot {
		public int index;
		public SavePlotTile[] tiles;
	}

	[System.Serializable]
	public class SavePlotTile {
		public int x;
		public int y;
		public SavePlotPatch patch;
		public SavePlotPlant plant;
	}

	[System.Serializable]
	public class SavePlotPatch {
		public float wetDuration;

		public SavePlotPatch(float wetDuration) {
			this.wetDuration = wetDuration;
		}
	}

	[System.Serializable]
	public class SavePlotPlant {
		public string plantCode;
		public int growthIndex;
		public float growthDuration;

		public SavePlotPlant() {
			this.plantCode = string.Empty;
			this.growthIndex = 0;
			this.growthDuration = 0f;
		}

		public SavePlotPlant(string plantCode, int growthIndex, float growthDuration) {
			this.plantCode = plantCode;
			this.growthIndex = growthIndex;
			this.growthDuration = growthDuration;
		}
	}
}