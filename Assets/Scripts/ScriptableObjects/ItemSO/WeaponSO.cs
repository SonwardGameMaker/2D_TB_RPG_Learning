using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/WeaponSO")]
[Serializable]
public class WeaponSO : ItemBaseSO
{
    public WeaponType WeaponType;
    public WeaponWeight WeaponWeight;
    public DamageType DamageType;
    public WeaponDamageParam WeaponDamage;
    public CharResource Durability;
    public bool IsBroken;
    public List<EquipAffectCharBaseSO> EquipAffectCharBase;
}
