using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using static UnityEditor.Timeline.Actions.MenuPriority;

namespace Game {
	public class GameTools : EditorWindow {

		[MenuItem("My Tools/Game Tools")]
		public static void ShowWindow() {
			GameTools editor = (GameTools)EditorWindow.GetWindow(typeof(GameTools));
			editor.titleContent = new GUIContent("Tools");
		}

		private void OnGUI() {
			if (Application.isPlaying) InPlayMode();
			else InEditorMode();
		}



		private void InPlayMode() {
			DrawInventory();
		}

		private void DrawInventory() {
			GUILayout.BeginVertical("box");
			GUILayout.Label("Inventory");

			DrawItems();
			DrawCoins();

			GUILayout.EndVertical();
		}

		private void DrawItems() {
			GUILayout.Label("Items");
			GUILayout.BeginVertical();

			List<Gameplay.Items.ItemPreset> allPresets = Gameplay.Items.ItemsManager.instance.GetAllItems();

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			for (int i = 0; i < allPresets.Count; i++) {
				Gameplay.Items.ItemPreset item = allPresets[i];
				AddItem(item);

				if ((i + 1) % 2 == 0) {
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
				}
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			void AddItem(Gameplay.Items.ItemPreset itemPreset) {
				float width = (position.width / 2) -10;
				GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.MinWidth(width));
				GUILayout.Label(string.Format("{0}", itemPreset.itemCode), new GUIStyle(EditorStyles.boldLabel));
				AddItem(1);
				AddItem(5);
				GUILayout.EndHorizontal();

				void AddItem(int quantity) {
					if (GUILayout.Button(string.Format("+{0}", quantity), GUILayout.Width(30))) {
						Gameplay.Inventory.InventoryManager.instance.AddItem(itemPreset, quantity);
					}
				}
			}
		}

		private void DrawCoins() {
			GUILayout.Label("Items");

			ModifyCoin(true, 0, 1, 2, 5, 10, 10, 25, 50, 100);
			ModifyCoin(false, 0, 1, 2, 5, 10, 10, 25, 50, 100);

			static void ModifyCoin(bool isIncrease, params int[] totalCoins) {
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				for (int i = 0; i < totalCoins.Length; i++) {
					string name = string.Format("{0} {1}", isIncrease ? "+" : "-", totalCoins[i]);
					if (GUILayout.Button(name, GUILayout.MinWidth(40))) {
						if (isIncrease) {
							Gameplay.Inventory.InventoryManager.instance.AddCoins(totalCoins[i]);
						} else {
							Gameplay.Inventory.InventoryManager.instance.RemoveCoins(totalCoins[i]);
						}
					}
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			}
		}

		private void InEditorMode() {
			DrawSave();
		}

		private void DrawSave() {
			GUILayout.BeginVertical("box");
			GUILayout.Label("Save");

			if (GUILayout.Button("Remove")) {
				PlayerPrefs.DeleteAll();
			}

			GUILayout.EndVertical();
		}
	}
}