using UnityEngine;

namespace Gameplay.Plots {
	public class PlotsManager : Singleton<PlotsManager> {

		[Header("Components")]
		[SerializeField] private Plot[] _plots = null;

		public Plot ActivePlot { get => _plots[0]; }

		private void OnEnable() {
			Game.Save.SaveManager.OnGameSave += OnGameSave;
			Game.Save.SaveManager.OnGameLoad += OnGameLoad;
		}

		private void OnDisable() {
			Game.Save.SaveManager.OnGameSave -= OnGameSave;
			Game.Save.SaveManager.OnGameLoad -= OnGameLoad;
		}

		private void OnGameSave(Game.Save.GameData gameData) {
			Game.Save.SavePlot[] savePlots = new Game.Save.SavePlot[_plots.Length];
			for (int i = 0; i < _plots.Length; i++) {
				savePlots[i] = new Game.Save.SavePlot {
					index = i,
					tiles = new Game.Save.SavePlotTile[_plots[i].TotalTiles()]
				};

				int tileIndex = 0;

				Tools.Grid<PlotTile> tileGrid = _plots[i].TileGrid;
				for (int y = 0; y < tileGrid.Height; y++) {
					for (int x = 0; x < tileGrid.Width; x++) {
						savePlots[i].tiles[tileIndex] = tileGrid.Get(x, y).Save();
						tileIndex++;
					}
				}
			}

		}

		private void OnGameLoad(Game.Save.GameData gameData) {

		}
	}
}