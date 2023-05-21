using UnityEngine;

namespace Game.Screens {
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class ScreenBase : MonoBehaviour {

		[Header("Info")]
		private protected RectTransform _rectTransform = null;
		private protected CanvasGroup _canvasGroup = null;
		private protected bool _isActive = false;


		public void Initialize() {
			_rectTransform = GetComponent<RectTransform>();
			_canvasGroup = GetComponent<CanvasGroup>();
			_rectTransform.anchoredPosition = Vector2.zero;

			OnInitialize();
			SetScreen(false);
		}

		public void Open() {
			SetScreen(true);
			OnOpen();
		}

		public void Close() {
			SetScreen(false);
			OnClose();
		}

		public abstract ScreenType GetScreenType();

		public virtual void OnInitialize() {

		}

		public virtual void OnOpen() {

		}

		public virtual void OnClose() {

		}


		private void SetScreen(bool value) {	
			_canvasGroup.alpha = value ? 1 : 0;
			_canvasGroup.interactable = value;
			_canvasGroup.blocksRaycasts = value;
			_isActive = value;
		}
	}
}