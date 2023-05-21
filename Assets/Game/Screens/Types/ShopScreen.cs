using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Screens {
	public class ShopScreen : ScreenBase {

		[Header("Components")]
		[SerializeField] private Image _background;

		public override ScreenType GetScreenType() => ScreenType.Shop;

		public override void OnOpen() {
			base.OnOpen();

			_background.color = Color.clear;
			_background.DOFade(.85f, .25f)
				.SetEase(Ease.OutSine);

		}

		private void Update() {
			if (!_isActive) {
				return;
			}

			if (Input.GetKeyDown(KeyCode.W)) {
				ScreenManager.instance.ChangeScreen(ScreenType.Gameplay);
			}
		}
	}
}