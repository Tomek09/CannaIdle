using UnityEditor;
using UnityEngine;

namespace Gameplay.Plots.Plants {
	[CustomPropertyDrawer(typeof(GrowthStage))]
	public class GrowthStageEditor : PropertyDrawerHelper {


		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty iconProperty = property.FindPropertyRelative("icon");
			SerializedProperty durationProperty = property.FindPropertyRelative("duration");

			EditorGUI.BeginProperty(position, label, property);

			Rect iconRect = GetRect(position, 0f, 0f, PropertyHeight(), PropertyHeight());
			Sprite icon = iconProperty.objectReferenceValue as Sprite;
			Texture texture = icon != null ? icon.texture : EditorGUIUtility.whiteTexture;
			EditorGUI.DrawTextureTransparent(iconRect, texture);

			EditorGUI.LabelField(LabelRect(position, 0), "Item");
			EditorGUI.ObjectField(PropertyRect(position, 0), iconProperty, GUIContent.none);

			EditorGUI.LabelField(LabelRect(position, 1), "Duration");
			durationProperty.floatValue = EditorGUI.FloatField(PropertyRect(position, 1), durationProperty.floatValue);

			EditorGUI.EndProperty();
		}
	}
}