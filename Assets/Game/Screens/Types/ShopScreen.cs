using DG.Tweening;
using Gameplay.Shops.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Screens {
	public class ShopScreen : ScreenBase {

		[Header("Components")]
		[SerializeField] private Image _background;
		[SerializeField] private CanvasGroup _shopPanelCanvas;

		[Header("Shop Components")]
		[SerializeField] private ShopManagerUI _shopManager;

		[Header("Navigation")]
		[SerializeField] private Button _nextPageButton;
		[SerializeField] private Button _previousPageButton;
		private int _currentPage;

		[Header("Buttons")]
		[SerializeField] private Button _closeButton = null;

		public override ScreenType GetScreenType() => ScreenType.Shop;

		public override void OnInitialize() {
			_closeButton.onClick.AddListener(OnCloseButton);
			_nextPageButton.onClick.AddListener(OnNextPage);
			_previousPageButton.onClick.AddListener(OnPreviousPage);
			_currentPage = 0;
		}

		public override void OnOpen() {
			base.OnOpen();
			FadeIn();
			_shopManager.OpenPage(_currentPage);
		}


		private void OnCloseButton() {
			ScreenManager.instance.ChangeScreen(ScreenType.Gameplay);
		}

		private void OnNextPage() {
			SetPage(1);
		}

		private void OnPreviousPage() {
			SetPage(-1);
		}

		private void SetPage(int direction) {
			_currentPage = Mathf.Clamp(_currentPage + direction, 0, 99);
			_shopManager.OpenPage(_currentPage);
		}

		private void FadeIn() {
			_background.color = Color.clear;
			_background.DOFade(.85f, .25f)
				.SetEase(Ease.OutSine);

			_shopPanelCanvas.alpha = 0;
			_shopPanelCanvas.DOFade(1f, .25f)
				.SetEase(Ease.OutSine);
		}
	}
}