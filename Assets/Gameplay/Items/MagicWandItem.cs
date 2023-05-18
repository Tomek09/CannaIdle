using UnityEngine;

namespace Gameplay.Items {
    [CreateAssetMenu(fileName = "Magic Wand Item - ", menuName = "SO/Items/Magic Wand Bag")]
    public class MagicWandItem : ItemPreset {

		[Header("Values")]
		[SerializeField] private float _power = 10f;

		public override void OnEquip() {
			Plots.PlotsManager.OnPlotTileMouseDown += OnPlotTileMouseDown;
		}

		public override void OnUnequip() {
			Plots.PlotsManager.OnPlotTileMouseDown -= OnPlotTileMouseDown;
		}

		private void OnPlotTileMouseDown(Plots.PlotTile tile) {
			if (tile.Plant.GetPlantPreset() == null) return;
			if (tile.Plant.IsFullyGrowth()) return;
			tile.Plant.AddGrowthDuration(_power);
		}
	}
}