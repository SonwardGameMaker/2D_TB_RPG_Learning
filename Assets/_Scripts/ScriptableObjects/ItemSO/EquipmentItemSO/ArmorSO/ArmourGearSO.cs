using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/Armour/ArmorSO")]
[Serializable]
public class ArmourGearSO : EquipmentItemSO
{
    [SerializeField] private List<DamageResistance> _damageResistances;
    [SerializeField] private CharResource _durability;
    [SerializeField] private bool _isBroken = false;
    
    public IReadOnlyList<DamageResistance> DamageResistances { get => _damageResistances; }
    public CharResourceInfo Durability { get => new CharResourceInfo(_durability); }
    public bool IsBroken { get =>  _isBroken; }

    public override List<ParInteractionCreator> GetParInteractionCreators()
    {
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
