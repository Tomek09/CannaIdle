using UnityEngine;

namespace Gameplay.Plots.Patches {
	public class Patch : MonoBehaviour {

		[Header("Info")]
		private PlotTile _tile = null;

		public void Initialize(PlotTile tile) {
			_tile = tile;
		}
	}
}