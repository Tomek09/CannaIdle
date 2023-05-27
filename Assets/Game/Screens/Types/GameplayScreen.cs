using UnityEngine;
using UnityEngine.UI;

namespace Game.Screens {
	public class GameplayScreen : ScreenBase {

		[Header("Buttons")]
		[SerializeField] private Button _shopButton = null;

		public override ScreenType GetScreenType() => ScreenType.Gameplay;

		public override void OnInitialize() {
			_shopButton.onClick.AddListener(OnShopButton);
		}

		private void OnShopButton() {
			ScreenManager.instance.ChangeScreen(ScreenType.Shop);
		}
	}
}