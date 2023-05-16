using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools {
	public class Grid<T> {

		[Header("Info")]
		private readonly int _width;
		private readonly int _height;
		private readonly float _size;
		private readonly Vector3 _origin;

		public int Width => _width;
		public int Height => _height;
		public float Size => _size;
		public Vector3 Origin => _origin;

		private readonly T[,] _grid;

		public Grid(int width, int height, float size, Vector3 origin, Func<Grid<T>, int, int, T> initalizeValue) {
			_width = width;
			_height = height;
			_size = size;
			_origin = origin;

			_grid = new T[width, height];
			for (int y = 0; y < _height; y++) {
				for (int x = 0; x < _width; x++) {
					_grid[x, y] = initalizeValue(this, x, y);
				}
			}
		}

		public void Set(int x, int y, T value) {
			if (IsValid(x, y))
				_grid[x, y] = value;
		}

		public void Set(Vector3 worldPosition, T value) {
			var coord = GetXY(worldPosition);
			Set(coord.Item1, coord.Item2, value);
		}

		public T Get(int x, int y) {
			if (IsValid(x, y)) return _grid[x, y];
			else return default;
		}

		public T Get(Vector3 worldPosition) {
			var coord = GetXY(worldPosition);
			return Get(coord.Item1, coord.Item2);
		}

		public List<T> GetNeighour(int x, int y, int depth = 1, bool skipSelf = true) {
			List<T> neighours = new List<T>();
			for (int j = -depth; j <= depth; j++) {
				for (int i = -depth; i <= depth; i++) {
					int newX = x + i;
					int newY = y + j;
					if (!IsValid(newX, newY) || skipSelf && Equals(newX, x) && Equals(newY, y)) continue;
					neighours.Add(Get(newX, newY));
				}
			}
			return neighours;
		}


		public (int, int) GetXY(Vector3 worldPosition) {
			Vector3 pos = worldPosition - _origin;
			int x = Mathf.FloorToInt(pos.x / _size);
			int y = Mathf.FloorToInt(pos.z / _size);

			return (x, y);
		}

		public Vector3 GetWorldPosition(int x, int y) {
			return new Vector3(x, 0f, y) * _size + _origin;
		}


		public bool IsValid(int x, int y) {
			return x >= 0 && y >= 0 && x < _width && y < _height;
		}
	}
}