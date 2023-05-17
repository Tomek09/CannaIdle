using UnityEngine;

namespace Gameplay.Items {
	[CreateAssetMenu(fileName = "Seed Bag Item - ", menuName = "SO/Items/Seed Bag")]
	public class SeedBagItem : ItemPreset {

		[Header("Seed")]
		public Plots.Plants.PlantPreset plantPreset;

		public override void OnEquip() { }
		public override void OnUnequip() { }
	}
}