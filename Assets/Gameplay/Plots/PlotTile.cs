using UnityEngine;

namespace Gameplay.Plots {
	public class PlotTile : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private Patches.Patch _patch = null;
		[SerializeField] private Plants.Plant _plant = null;

		public Patches.Patch Patch { get => _patch; }
		public Plants.Plant Plant { get => _plant; }

		[Header("Info")]
		private int _x;
		private int _y;

		public void Initialize(int x, int y) {
			_x = x;
			_y = y;

			_patch.Initialize(this);
			_plant.Initialize(this);
		}


		public Game.Save.SavePlotTile Save() {
			return new Game.Save.SavePlotTile() {
				x = _x,
				y = _y,
				patch = _patch.Save(),
				plant = _plant.Save()
			};
		}

		public void Load(Game.Save.SavePlotTile savePlotTile) {

		}
	}
}