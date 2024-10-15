using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DsUtils
{
    public static T GetDataSystem<T>(List<CharacterDataSystemBase> systems) where T : class
        => systems.Find(o => o is T) as T;
}
