using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Screens {
	public class ShopScreen : ScreenBase {

		[Header("Components")]
		[SerializeField] private Image _background;
		[SerializeField] private CanvasGroup _shopPanelCanvas;

		[Header("Buttons")]
		[SerializeField] private Button _closeButton = null;

		public override ScreenType GetScreenType() => ScreenType.Shop;

		public override void OnInitialize() {
			_closeButton.onClick.AddListener(OnCloseButton);
		}

		public override void OnOpen() {
			base.OnOpen();

			_background.color = Color.clear;
			_background.DOFade(.85f, .25f)
				.SetEase(Ease.OutSine);

			_shopPanelCanvas.alpha = 0;
			_shopPanelCanvas.DOFade(1f, .25f)
				.SetEase(Ease.OutSine);
		}


		private void OnCloseButton() {
			ScreenManager.instance.ChangeScreen(ScreenType.Gameplay);
		}
	}
}