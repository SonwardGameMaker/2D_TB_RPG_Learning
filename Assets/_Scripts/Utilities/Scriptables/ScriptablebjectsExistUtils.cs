using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptablebjectsExistUtils
{
    public static bool IsDynamicallyGenerated<T>(T so) where T : ScriptableObject
        => !AssetDatabase.Contains(so);
}
