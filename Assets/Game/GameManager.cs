using UnityEngine;

namespace Game {
	public class GameManager : Singleton<GameManager> {

		public static System.Action OnGameTick;

		[Header("Ticks")]
		private float _nextTick;
		private float _tickDuration = 1f;

		private void Start() {
			// TODO > Wyci¹gnaæ to do osobnego skryptu GameTick
			_nextTick = Time.time + _tickDuration;
		}

		private void Update() {
			if (Time.time >= _nextTick) {
				_nextTick = Time.time + _tickDuration;
				OnGameTick?.Invoke();
			}
		}
	}
}