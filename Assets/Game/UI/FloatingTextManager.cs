using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Global.UI {
	public class FloatingTextManager : Singleton<FloatingTextManager> {

		[Header("Prefab")]
		[SerializeField] private TMP_Text _textPrefab = null;
		[SerializeField] private Transform _textParent = null;

		private protected override void Awake() {
			base.Awake();
			Debug.Log("[TODO] Pooling system!");
		}

		public void CreateFloatingText(Vector3 position, FloatingTextSettings settings) {
			Vector3 spawnPosition = position;
			if (settings.randomizePosition) {
				Vector2 randomPoint = Random.insideUnitCircle * 2f;
				spawnPosition += new Vector3(randomPoint.x, randomPoint.y);
			}

			TMP_Text text = GameObjects.GOInstantiate(_textPrefab, _textParent);
			text.rectTransform.position = spawnPosition;
			text.rectTransform.DOAnchorPosY(spawnPosition.y + 20, settings.fadeDuration)
				.SetDelay(settings.displayDuration)
				.SetEase(Ease.OutQuad);

			text.DOFade(0f, settings.fadeDuration)
				.SetDelay(settings.displayDuration)
				.SetEase(Ease.OutQuad);

			SetupTextVisual();
			
			void SetupTextVisual() {
				text.text = settings.text;
				text.color = settings.color;
			}
		}
	}
}