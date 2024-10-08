using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class DamagableBaseSO : ScriptableObject
{
    public abstract (bool, float, Damage) TakeHit(CharacterBlank character, HitDataContainer hit);
    public abstract Damage TakeDamage(CharacterBlank character, Damage damage);
    public abstract void TakeHealing(CharacterBlank character, float amount);

    protected float CalculateDamageTaken(OtherParameters parameters, Damage damage)
    {
        DamageResistance currentResistance = parameters.GetDamageResistanceByType(damage.DamageType);

        float trashholdedDamage = damage.Amount - currentResistance.Trashhold;
        trashholdedDamage = trashholdedDamage >= 0 ? trashholdedDamage : 0;

        float mitigatedDamage = damage.Amount - damage.Amount * currentResistance.Mitigation;
        mitigatedDamage = mitigatedDamage >= 0 ? mitigatedDamage : 0;

        return mitigatedDamage < trashholdedDamage ? mitigatedDamage : trashholdedDamage;
    }
}
