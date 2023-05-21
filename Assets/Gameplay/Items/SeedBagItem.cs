using UnityEngine;

namespace Gameplay.Items {
	[CreateAssetMenu(fileName = "Seed Bag Item - ", menuName = "SO/Items/Seed Bag")]
	public class SeedBagItem : ItemPreset {

		[Header("Seed")]
		public Plots.Plants.PlantPreset plantPreset;

		public override void OnEquip() {
			Plots.PlotsManager.OnPlotTileMouseDown += OnPlotTileMouseDown;
		}
		
		public override void OnUnequip() {
			Plots.PlotsManager.OnPlotTileMouseDown -= OnPlotTileMouseDown;
		}

		private void OnPlotTileMouseDown(Plots.PlotTile tile) {
			if (tile.Plant.GetPlantPreset() != null) {
				return;
			}

			tile.Plant.SetPlant(plantPreset);
			Inventory.InventoryManager.instance.RemoveItem(this);
		}
	}
}