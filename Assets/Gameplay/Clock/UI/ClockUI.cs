using UnityEngine;

namespace Gameplay.Times.UI {
	public class ClockUI : MonoBehaviour {
		[Header("Components")]
		[SerializeField] private RectTransform handRect = null;

		private void Update() {
			handRect.localEulerAngles += 2f * Time.deltaTime * Vector3.back;
		}
	}
}