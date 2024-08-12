using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatInfo
{
    private Stat _stat;

    public StatInfo(Stat stat)
    {
        _stat = stat;
    }

    #region properties
    public string Name { get => _stat.Name; }
    public float BaseValue { get => _stat.CurrentValueBase; }
    public float CurrentValue { get => _stat.CurrentValue; }
    #endregion
}
