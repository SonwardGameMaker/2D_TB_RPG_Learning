using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ModVar))]
public class ModVarInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty baseValueProperty = property.FindPropertyRelative("baseValue");
        baseValueProperty.floatValue = EditorGUI.FloatField(position, label, baseValueProperty.floatValue);
    }
}
