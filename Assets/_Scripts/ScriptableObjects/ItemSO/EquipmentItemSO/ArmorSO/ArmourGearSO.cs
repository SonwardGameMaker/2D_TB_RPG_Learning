using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/Armour/ArmorSO")]
[Serializable]
public class ArmourGearSO : EquipmentItemSO
{
    [SerializeField] private List<DamageResistance> _damageResistances;
    [SerializeField] private bool _isBroken = false;
    
    public IReadOnlyList<DamageResistance> DamageResistances { get => _damageResistances; }
    public bool IsBroken { get =>  _isBroken; }

    public override List<ParInteractionCreator> GetParInteractionCreators()
    {
        if (_damageResistances == null || _damageResistances.Count  == 0)
            return new List<ParInteractionCreator>();

        List<ParInteractionCreator> result = new List<ParInteractionCreator>();

        foreach (DamageResistance damageResistance in _damageResistances)
            result.AddRange(new List<ParInteractionCreator>
            {
                new DamResistanceEffectorCreator(damageResistance.Mitigation, ModifierType.Flat, damageResistance.DamageType, DamageResistanceType.Mitigation),
                new DamResistanceEffectorCreator(damageResistance.Trashhold, ModifierType.Flat, damageResistance.DamageType, DamageResistanceType.Trashhold)
            });
        base.GetParInteractionCreators();

        return result;
    }

    public override Item CreateItem() => new ArmourGear(this);
}
