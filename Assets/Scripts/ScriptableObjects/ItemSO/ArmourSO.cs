using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/ArmorSO")]
[Serializable]
public class ArmourSO : ItemBaseSO
{
    public ArmorType _armourType;
    public List<DamageResistance> _damageResistances;
    public CharResource _durability;
    public bool _isBroken = false;
    public List<EquipAffectCharBaseSO> _equipAffectCharBase;
}
