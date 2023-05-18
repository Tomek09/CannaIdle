using UnityEngine;

namespace Gameplay.Items {
    [CreateAssetMenu(fileName = "Watering Can Item - ", menuName = "SO/Items/Watering Can")]
    public class WateringCanItem : ItemPreset {

		public override void OnEquip() {
			Plots.PlotsManager.OnPlotTileMouseDown += OnPlotTileMouseDown;
		}

		public override void OnUnequip() {
			Plots.PlotsManager.OnPlotTileMouseDown -= OnPlotTileMouseDown;
		}

		private void OnPlotTileMouseDown(Plots.PlotTile tile) {
			Debug.Log("[TODO]");
		}
	}
}