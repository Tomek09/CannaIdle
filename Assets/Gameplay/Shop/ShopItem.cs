using UnityEngine;

namespace Gameplay.Items {
	public class ShopItem : ScriptableObject {

		public ItemPreset item;
		public int cost;


#if UNITY_EDITOR
		[UnityEditor.MenuItem("Assets/Create/SO/Items/Shop Item")]
		private static void Create() {
			string folderPath = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeInstanceID);
			if (folderPath.Contains("."))
				folderPath = folderPath.Remove(folderPath.LastIndexOf('/'));

			Object activeObject = UnityEditor.Selection.activeObject;
			ItemPreset itemPreset = null;
			string assetName = $"Shop Item - .asset";
			if (activeObject != null && activeObject is ItemPreset item) {
				itemPreset = item;
				assetName = string.Format("Shop Item - {0}.asset", itemPreset.displayName);
			}

			ShopItem shopItem = UnityEditor.ObjectFactory.CreateInstance<ShopItem>();

			if (itemPreset != null) {
				shopItem.item = itemPreset;
			}

			string path = System.IO.Path.Combine(folderPath, assetName);
			UnityEditor.AssetDatabase.CreateAsset(shopItem, path);

			UnityEditor.Selection.activeObject = shopItem;

		}
#endif

	}
}