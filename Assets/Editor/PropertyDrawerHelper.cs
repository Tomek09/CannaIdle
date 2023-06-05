using UnityEditor;
using UnityEngine;

public class PropertyDrawerHelper : PropertyDrawer {

	protected Rect GetRect(Rect position, float xOffset, float yOffset, float width, float height) {
		return new Rect(position.x + xOffset, position.y + yOffset, width, height);
	}

	protected Rect LabelRect(Rect position, int y) {
		return GetRect(position, PropertyHeight() + GUIHeight(), GUIHeight() * y, LabelWidth(), GUIHeight());
	}

	protected Rect PropertyRect(Rect position, int y) {
		return GetRect(position, PropertyHeight() + GUIHeight() + LabelWidth(), GUIHeight() * y, position.width - PropertyHeight() - GUIHeight() - LabelWidth(), GUIHeight());
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => PropertyHeight();

	protected virtual float PropertyHeight() => 60f;

	protected virtual float GUIHeight() => 20f;

	protected virtual float LabelWidth() => 75;
}
