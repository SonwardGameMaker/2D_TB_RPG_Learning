using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerIngameController : CharacterIngameController
{
    private CharacterStatsSystem _stats;
    private CharHealthSystem _healthSystem;

    public new void Start()
    {
        base.Start();
        _stats = _character.Stats;
        _healthSystem = _character.Health;
    }

    public void UpDownAttribute(string attributeName, bool increase)
    {
        Stat stat = (Stat)_stats.GetType().GetProperty(attributeName).GetValue(_stats);
        _stats.UpDownAttribute(stat, increase);
    }
}
