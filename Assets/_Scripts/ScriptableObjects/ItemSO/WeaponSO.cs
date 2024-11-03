using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/WeaponSO")]
[Serializable]
internal class WeaponSO : ItemBaseSO
{
    public AttackRangeType WeaponRangeType;
    public WeaponType WeaponWeight;
    public DamageType DamageType;
    public WeaponDamageParam WeaponDamage;
    public int WeaponRange;
    public CharResource Durability;
    public bool IsBroken;
    public List<EquipAffectCharBaseSO> EquipAffectCharBase;

    private void Start()
    {
        if (WeaponRange <= 0)
            WeaponRange = 1;
    }
}
