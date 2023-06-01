using UnityEngine;

namespace Global.UI {
	public struct FloatingTextSettings {

		public string text;
		public Color color;

		public bool randomizePosition;
		public int randomizePositionPower;


		public FloatingTextSettings(string text, Color color, bool randomizePosition, int randomizePositionPower = 50) {
			this.text = text;
			this.color = color;

			this.randomizePosition = randomizePosition;
			this.randomizePositionPower = randomizePositionPower;
		}

	}
}