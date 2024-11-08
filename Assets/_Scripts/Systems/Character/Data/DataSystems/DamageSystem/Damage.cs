using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    private float _amount;
    private DamageType _damageType;
    private bool _isDicrect; // Direct damage ignores all resistances
    private float _penetrationPercent; // Percent of redused amount of damage that will be ignored (this value calculates after final calculation of mitigation was calculated)
    private float _critChancePercent; // This will be triggered only after succesfull hit
    private float _critDamageBonusPercent;

    public Damage(float amount, DamageType type, bool isDirect = false, float penetrationPercent = 0, float critChancePercent = 0, float critDamageBonusPercent = 0)
    {
        _amount = amount;
        _damageType = type;
        _isDicrect = isDirect;
        _penetrationPercent = penetrationPercent;
        _critChancePercent = critChancePercent;
        _critDamageBonusPercent = critDamageBonusPercent;
    }

    public float Amount { get => _amount;  }
    public DamageType DamageType { get => _damageType; } 
    public bool IsDirect { get => _isDicrect; }
    public float PenetrationPercent { get => _penetrationPercent; }
    public float CritChancePercents { get => _critChancePercent; }
    public float CritDamageBonusPercents { get => _critDamageBonusPercent; }
}
