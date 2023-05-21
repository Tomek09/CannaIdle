using UnityEngine;

namespace Game.Screens {
	public class GameplayScreen : ScreenBase {

		public override ScreenType GetScreenType() => ScreenType.Gameplay;

		private void Update() {
			if (!_isActive) {
				return;
			}

			if (Input.GetKeyDown(KeyCode.Q)) {
				ScreenManager.instance.ChangeScreen(ScreenType.Shop);
			}
		}
	}
}