using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ItemSoUtilities
{
    public static bool IsItemSoNameIsNullOrEmpty<T>(T itemSO) where T : ItemSO
        => string.IsNullOrEmpty(itemSO.Name);
}
