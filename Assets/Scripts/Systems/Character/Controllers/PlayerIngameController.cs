using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerIngameController : CharacterIngameController
{
    private CharacterStatsSystem _stats;

    public new void Start()
    {
        base.Start();
        _stats = GetComponent<CharacterBlank>().Stats;
    }

    public void UpDownAttribute(string attributeName, bool increase)
    {
        Stat stat = (Stat)_stats.GetType().GetProperty(attributeName).GetValue(_stats);
        _stats.UpDownAttribute(stat, increase);
    }
}
