using UnityEngine;

namespace Gameplay.Plots {
	public class PlotSize : MonoBehaviour {

		private const float TILE_SIZE = 1.1f;

		[Header("Settings")]
		[SerializeField] private int _width;
		[SerializeField] private int _height;

		public int Width { get => _width; }
		public int Height { get => _height; }
		public float TileSize { get => TILE_SIZE; }
		public Vector3 Origin { get => Tools.Extensions.GridCenter(_width, _height, TILE_SIZE, transform.position); }

		private void OnDrawGizmos() {
			Vector3 size = new Vector3(1, 0f, 1) * TILE_SIZE;
			for (int y = 0; y < _height; y++) {
				for (int x = 0; x < _width; x++) {
					Vector3 position = new Vector3(x, 0f, y) * TILE_SIZE + Origin;

					Gizmos.color = Color.black;
					Gizmos.DrawWireCube(position, size);
				}
			}
		}
	}
}