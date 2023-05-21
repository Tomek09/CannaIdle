using UnityEngine;

namespace Gameplay.Items {
    [CreateAssetMenu(fileName = "Gloves Item - ", menuName = "SO/Items/Gloves Bag")]
    public class GlovesItem : ItemPreset {

		public override void OnEquip() {
			Plots.PlotsManager.OnPlotTileMouseDown += OnPlotTileMouseDown;
		}

		public override void OnUnequip() {
			Plots.PlotsManager.OnPlotTileMouseDown -= OnPlotTileMouseDown;
		}

		private void OnPlotTileMouseDown(Plots.PlotTile tile) {
			if (tile.Plant.GetPlantPreset() == null) {
				return;
			}

			if (!tile.Plant.IsFullyGrowth()) {
				return;
			}

			tile.Plant.RemovePlant();
		}
	}
}