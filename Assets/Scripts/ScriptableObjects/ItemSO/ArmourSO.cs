using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/ArmorSO")]
[Serializable]
internal class ArmourSO : ItemBaseSO
{
    public ArmorType ArmourType;
    public List<DamageResistance> DamageResistances;
    public CharResource Durability;
    public bool IsBroken = false;
    public List<EquipAffectCharBaseSO> EquipAffectCharBase;
}
