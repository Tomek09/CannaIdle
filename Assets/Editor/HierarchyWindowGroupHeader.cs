using UnityEngine;
using UnityEditor;

/// <summary>
/// Hierarchy Window Group Header
/// http://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header + Some modifiers
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGroupHeader {

	static HierarchyWindowGroupHeader() {
		EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
	}

	static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {
		var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

		if (gameObject != null && gameObject.name.StartsWith("---", System.StringComparison.Ordinal)) {
			EditorGUI.DrawRect(selectionRect, BackgroundColor());
			string output = gameObject.name.Replace("-", "");
			string content = string.Format("<b>{0}</b>", output).ToUpperInvariant();
			EditorGUI.DropShadowLabel(selectionRect, content, ShadowStyle());
		}
	}


	static Color BackgroundColor() => new Color(.15f, .15f, .15f);

	static GUIStyle ShadowStyle() {
		return new GUIStyle(EditorStyles.label) {
			richText = true,
			alignment = TextAnchor.MiddleCenter,
			normal = new GUIStyleState() { textColor = Color.yellow },
		};
	}
}