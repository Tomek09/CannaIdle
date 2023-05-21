using System.Collections.Generic;
using UnityEngine;

namespace Game.Screens {
	public class ScreenManager : Singleton<ScreenManager> {


		[Header("Components")]
		[SerializeField] private ScreenBase[] _screens = null;

		[Header("Values")]
		[SerializeField] private ScreenType _initScreen;

		[Header("Info")]
		private Dictionary<ScreenType, ScreenBase> _screensPerType = new Dictionary<ScreenType, ScreenBase>();
		private ScreenType _currentScreen;

		private protected override void Awake() {
			base.Awake();
			_screensPerType = new Dictionary<ScreenType, ScreenBase>();
			foreach (var screen in _screens) {
				_screensPerType.Add(screen.GetScreenType(), screen);
				screen.Initialize();
			}
		}

		private void Start() {
			_currentScreen = _initScreen;
			_screensPerType[_currentScreen].Open();
		}

		public void ChangeScreen(ScreenType screenTarget) {
			_screensPerType[_currentScreen].Close();
			_currentScreen = screenTarget;
			_screensPerType[_currentScreen].Open();
		}

		public T GetScreen<T>(ScreenType screenType) where T : ScreenBase {
			return GetScreen(screenType) as T;
		}

		public ScreenBase GetScreen(ScreenType screenType) {
			return _screensPerType[screenType];
		}
	}
}