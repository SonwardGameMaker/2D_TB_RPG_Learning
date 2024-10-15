using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DsUtils
{
    public static T GetDataSystem<T>(List<object> list) where T : class
        => list.Find(o => o is T) as T;
}
