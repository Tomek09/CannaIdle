using UnityEngine;

namespace Global.UI {
	public struct FloatingTextSettings {

		public string text;
		public Color color;

		public float displayDuration;
		public float fadeDuration;
		public bool randomizePosition;


		public FloatingTextSettings(string text, Color color, float displayDuration, float fadeDuration, bool randomizePosition) {
			this.text = text;
			this.color = color;

			this.displayDuration = displayDuration;
			this.fadeDuration = fadeDuration;
			this.randomizePosition = randomizePosition;
		}

	}
}