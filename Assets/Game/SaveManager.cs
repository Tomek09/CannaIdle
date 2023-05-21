using UnityEngine;

namespace Game.Save {
	public class SaveManager : Singleton<SaveManager> {

		private const string SAVE_NAME = "SAVE_1";

		public static System.Action<GameData> OnGameSave;
		public static System.Action<GameData> OnGameLoad;

		private void OnApplicationQuit() {
			SaveGame();
		}

		private void OnApplicationPause(bool pause) {
			if (pause) SaveGame();
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.F1)) SaveGame();
			else if (Input.GetKeyDown(KeyCode.F2)) LoadGame();
		}

		public void SaveGame() {
			Debug.Log("SaveGame");

			GameData gameData = new GameData();
			OnGameSave?.Invoke(gameData);

			string json = JsonUtility.ToJson(gameData);
			Debug.Log(json);	
			PlayerPrefs.SetString(SAVE_NAME, json);
		}

		public void LoadGame() {
			Debug.Log("LoadGame");

			GameData gameData;
			if (PlayerPrefs.HasKey(SAVE_NAME)) {
				string json = PlayerPrefs.GetString(SAVE_NAME);
				gameData = JsonUtility.FromJson<GameData>(json);
			} else {
				gameData = new GameData();
			}

			OnGameLoad?.Invoke(gameData);
		}
	}
}