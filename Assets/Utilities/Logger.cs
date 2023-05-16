using UnityEngine;

namespace Tools {
	public class Logger : MonoBehaviour {

		[Header("Settings")]
		[SerializeField] private bool _enabled = true;
		[SerializeField] private string _prefix = string.Empty;
		[SerializeField] private Color _logColor = Color.white;

		public void Log(string message, Object context = null) {
			if (!_enabled) return;

			string format = "<color={0}>[{1}]</color> {2}";
			string color = Extensions.ColorToHex(_logColor);
			string output = string.Format(format, color, _prefix, message);
			Debug.Log(output, context);
		}
	}
}