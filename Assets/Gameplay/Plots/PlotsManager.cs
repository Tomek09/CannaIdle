using UnityEngine;

namespace Gameplay.Plots {
	public class PlotsManager : Singleton<PlotsManager> {

		private readonly Vector3 MOUSE_OFFSET = new Vector3(.5f, 0f, .5f);

		[Header("Components")]
		[SerializeField] private Plot[] _plots = null;

		public Plot ActivePlot { get => _plots[0]; }

		public static System.Action<PlotTile> OnPlotTileMouseDown;

		private void OnEnable() {
			Game.InputHandler.OnMouseButtonDown += OnMouseButtonDown;
			Game.Save.SaveManager.OnGameSave += OnGameSave;
			Game.Save.SaveManager.OnGameLoad += OnGameLoad;
		}

		private void OnDisable() {
			Game.InputHandler.OnMouseButtonDown -= OnMouseButtonDown;
			Game.Save.SaveManager.OnGameSave -= OnGameSave;
			Game.Save.SaveManager.OnGameLoad -= OnGameLoad;
		}

		private protected override void Awake() {
			base.Awake();
			for (int i = 0; i < _plots.Length; i++) {
				_plots[i].Initialize();
			}
		}

		private void Start() {
			Game.Save.SaveManager.instance.LoadGame();
		}

		private void OnMouseButtonDown(Vector3 worldPosition) {
			if (!ActivePlot.TileGrid.TryGet(worldPosition + MOUSE_OFFSET, out PlotTile tile)) return;
			OnPlotTileMouseDown?.Invoke(tile);
		}

		private Plot GetPlot(int index) => _plots[index];

		#region Save/Load

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

			gameData.plots = savePlots;
		}

		private void OnGameLoad(Game.Save.GameData gameData) {
			Game.Save.SavePlot[] savePlots = gameData.plots;
			foreach (Game.Save.SavePlot savePlot in savePlots) {
				Plot plot = GetPlot(savePlot.index);
				Tools.Grid<PlotTile> tileGrid = plot.TileGrid;
				foreach (Game.Save.SavePlotTile savePlotTile in savePlot.tiles) {
					if (!tileGrid.TryGet(savePlotTile.x, savePlotTile.y, out PlotTile tile)) continue;
					tile.Load(savePlotTile);
				}
			}
		}

		#endregion

	}
}