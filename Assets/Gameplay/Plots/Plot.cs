using Tools;
using UnityEngine;

namespace Gameplay.Plots {
	public class Plot : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private PlotSize _size = null;

		[Header("Prefab")]
		[SerializeField] private PlotTile _tilePrefab = null;
		[SerializeField] private Transform _tileParent = null;

		[Header("Info")]
		private Grid<PlotTile> _tiles = null;
		
		public Grid<PlotTile> TileGrid { get => _tiles; }


		public void Initialize() {
			_tiles = new Grid<PlotTile>(_size.Width, _size.Height, _size.TileSize, _size.Origin, (Grid<PlotTile> g, int x, int y) => CreateTile(g, x, y));
		}

		private PlotTile CreateTile(Grid<PlotTile> g, int x, int y) {
			PlotTile plotTile = GameObjects.GOInstantiate(_tilePrefab, g.GetWorldPosition(x, y), Vector3.zero, _tileParent);
			plotTile.Initialize(this, x, y);
			return plotTile;
		}

		public int TotalTiles() => _tiles.Width * _tiles.Height;
	}
}