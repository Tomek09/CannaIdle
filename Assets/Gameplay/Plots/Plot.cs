using Tools;
using UnityEngine;

namespace Gameplay.Plots {
	public class Plot : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private PlotSize _size = null;

		[Header("Prefab")]
		[SerializeField] private PlotTile _tilePrefab = null;
		[SerializeField] private Transform _tileParent = null;

		[Header("Debug")]
		[SerializeField] private Plants.PlantPreset _preset = null;

		[Header("Info")]
		private Grid<PlotTile> _tiles = null;

		private void OnEnable() {
			Game.InputHandler.OnMouseButtonDown += OnMouseButtonDown;
		}

		private void OnDisable() {
			Game.InputHandler.OnMouseButtonDown -= OnMouseButtonDown;
		}

		private void Start() {
			Initialize();

			_tiles.Get(0, 0).Plant.SetPlant(_preset, 0, 0);
		}

		public void Initialize() {
			_tiles = new Grid<PlotTile>(_size.Width, _size.Height, _size.TileSize, _size.Origin, (Grid<PlotTile> g, int x, int y) => CreateTile(g, x, y));
		}

		private PlotTile CreateTile(Grid<PlotTile> g, int x, int y) {
			PlotTile plotTile = GameObjects.GOInstantiate(_tilePrefab, g.GetWorldPosition(x, y), Vector3.zero, _tileParent);
			plotTile.Initialize();
			return plotTile;
		}

		private void OnMouseButtonDown(Vector3 worldPosition) {
			PlotTile tile = _tiles.Get(worldPosition);
			if (tile == null) return;

			Debug.Log("A");
		}
	}
}