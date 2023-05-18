using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

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

			GUILayout.Label("Items");
			DrawItems();

			GUILayout.EndVertical();
		}

		private void DrawItems() {
			GUILayout.BeginVertical();

			List<Gameplay.Items.ItemPreset> allPresets = Gameplay.Items.ItemsManager.instance.GetAllItems();

			for (int i = 0; i < allPresets.Count; i++) {
				Gameplay.Items.ItemPreset item = allPresets[i];
				AddItem(item);
			}

			GUILayout.EndVertical();

			void AddItem(Gameplay.Items.ItemPreset itemPreset) {
				if (GUILayout.Button(string.Format("{0}", itemPreset.itemCode))) {
					Gameplay.Inventory.InventoryManager.instance.AddItem(itemPreset, 1);
				}
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