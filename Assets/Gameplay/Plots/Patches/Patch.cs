using UnityEngine;

namespace Gameplay.Plots.Patches {
	public class Patch : MonoBehaviour {

		[Header("Info")]
		private PlotTile _tile = null;

		public void Initialize(PlotTile tile) {
			_tile = tile;
		}

		public Game.Save.SavePlotPatch Save() {
			Debug.Log("TODO");
			return new Game.Save.SavePlotPatch();
		}

		public void Load(Game.Save.SavePlotPatch patch) {
			Debug.Log("TODO");
		}
	}
}