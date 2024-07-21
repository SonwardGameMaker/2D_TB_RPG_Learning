using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Mechanical, Electrical, Energy, Poison }
public class Damage
{
    private float _amount;
    private DamageType _damageType;

    public Damage(float amount, DamageType type)
    {
        _amount = amount;
        _damageType = type;
    }

    public float Amount { get => _amount;  }
    public DamageType Type { get => _damageType; } 
}
