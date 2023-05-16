using System.Collections.Generic;
using UnityEngine;

namespace Tools {
    public static class Extensions {

        public static string ColorToHex(Color color) {
            return "#" + ColorUtility.ToHtmlStringRGBA(color);
		}


		public static float GetRandom(this Vector2 range) {
			return Random.Range(range.x, range.y);
		}

		public static int GetRandom(this Vector2Int range) {
			return Random.Range(range.x, range.y);
		}


		public static T GetRandom<T>(this T[] array) {
			if (array.Length == 0) return default;
			if (array.Length == 1) return array[0];
			return array[Random.Range(0, array.Length)];
		}

		public static T GetRandom<T>(this List<T> list) {
			if (list.Count == 0) return default;
			if (list.Count == 1) return list[0];
			return list[Random.Range(0, list.Count)];
		}


		public static void Shuffle<T>(this T[] array) {
			var total = array.Length;
			var last = total - 1;
			for (var i = 0; i < last; ++i) {
				int r = Random.Range(i, total);
				(array[r], array[i]) = (array[i], array[r]);
			}
		}

		public static void Shuffle<T>(this List<T> list) {
			var total = list.Count;
			var last = total - 1;
			for (var i = 0; i < last; ++i) {
				int r = Random.Range(i, total);
				(list[r], list[i]) = (list[i], list[r]);
			}
		}


		public static T GetRandomEnum<T>() where T : System.Enum {
			System.Array array = System.Enum.GetValues(typeof(T));
			T[] tArray = new T[array.Length];
			for (int i = 0; i < tArray.Length; i++)
				tArray[i] = (T)array.GetValue(i);

			return tArray.GetRandom();
		}


		public static Vector3 GridCenter(int width, int height, float tileSize, Vector3 origin) {
			float sizeWidth = (width * tileSize) / 2;
			float sizeHeight = (height * tileSize) / 2;
			float halfTile = tileSize / 2;

			return new Vector3(-sizeWidth + halfTile, 0f, -sizeHeight + halfTile) + origin;
		}
	}
}