//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(IngameGrid))]
//public class IngameGridInspector : Editor
//{
//    private SerializedProperty _width;
//    private SerializedProperty _height;

//    private void OnEnable()
//    {
//        _width = serializedObject.FindProperty("_width");
//        _height = serializedObject.FindProperty("_height");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        DrawDefaultInspector();

//        EditorGUILayout.PropertyField(_width);
//        EditorGUILayout.PropertyField(_height);

//        IngameGrid grid = (IngameGrid)target;

//        if (GUILayout.Button("Reset grid"))
//        {
//            int width = _width.intValue;
//            int height = _height.intValue;

//            grid.SetGrid(width, height);
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}
