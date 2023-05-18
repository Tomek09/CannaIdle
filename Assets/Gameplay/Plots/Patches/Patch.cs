using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Plots.Patches {
	public class Patch : MonoBehaviour {

		private enum State {
			Dry = 0,
			Wet = 1
		}

		[System.Serializable]
		private struct StateSettings {
			public State state;
			public Sprite sprite;
		}

		[Header("Components")]
		[SerializeField] private SpriteRenderer _spriteRenderer = null;

		[Header("Values")]
		[SerializeField] private StateSettings[] _states = null;

		[Header("Info")]
		private Dictionary<State, StateSettings> _statesByType = null;
		private PlotTile _tile = null;
		private State _currentState;
		private float _wetDuration;

		public void Initialize(PlotTile tile) {
			_tile = tile;
			_statesByType = new Dictionary<State, StateSettings>();
			for (int i = 0; i < _states.Length; i++) {
				_statesByType.Add(_states[i].state, _states[i]);
			}
			_currentState = State.Dry;
			_wetDuration = 0f;
		}

		public void SetWetDuration(float wetDuration) {
			_wetDuration = Mathf.Clamp(wetDuration, 0, float.MaxValue);

			if (_wetDuration <= 0) {
				ChangeState(State.Dry);
				return;
			}

			if (_currentState == State.Dry) {
				ChangeState(State.Wet);
				Game.GameManager.OnGameTick += OnGameTick;
			}
		}

		private void ChangeState(State state) {
			_currentState = state;
			_spriteRenderer.sprite = _statesByType[state].sprite;
		}

		private void OnGameTick() {
			_wetDuration = Mathf.Clamp(_wetDuration - Time.deltaTime, 0, float.MaxValue);

			if(_wetDuration <= 0) {
				ChangeState(State.Dry);
				Game.GameManager.OnGameTick -= OnGameTick;
			}
		}

		#region Save/Load

		public Game.Save.SavePlotPatch Save() {
			return new Game.Save.SavePlotPatch(_wetDuration);
		}

		public void Load(Game.Save.SavePlotPatch patch) {
			SetWetDuration(patch.wetDuration);
		}

		#endregion
	}
}