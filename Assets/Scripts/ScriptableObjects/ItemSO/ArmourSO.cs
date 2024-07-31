using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/ArmorSO")]
public class ArmourSO : ItemBaseSO
{
    [SerializeField] private List<DamageResistance> _damageResistances;
    [SerializeField] private CharResource _durability;
    [SerializeField] private bool _isBroken;
    [SerializeField] private List<EquipAffectCharBaseSO> _equipAffectCharBases;
}
