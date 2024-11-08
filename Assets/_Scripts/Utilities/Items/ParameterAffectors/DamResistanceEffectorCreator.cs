using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DamResistanceEffectorCreator : ParInteractionCreator
{
    public DamageType DamageType;
    public DamageResistanceType DamageResistanceType;

    #region init
    public DamResistanceEffectorCreator() { }
    public DamResistanceEffectorCreator(float modifierAmount, ModifierType modifierType, DamageType damageType, DamageResistanceType damageResistanceType) : base(modifierAmount, modifierType)
    {
        DamageType = damageType;
        DamageResistanceType = damageResistanceType;
    }
    public DamResistanceEffectorCreator(DamResistanceEffectorCreator other) : this(other.ModifierAmount, other.ModifierType, other.DamageType, other.DamageResistanceType) { }
    #endregion

    #region external interactions
    public override ParInteraction CreateParInteraction(CharacterBlank character)
        => character.IngameParameters.CreateDamageResistanceEffect(DamageType, CreateFlatParemFromFloat(), ItemAffectResourceLogic);
    #endregion

    #region internal operations
    protected override (float, ModifierType) CalculationLogic(CharParameterBase affector)
        => new(ModifierAmount, ModifierType);

    private void ItemAffectResourceLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
    {
        var myDelegate = GetValueByEnum();

        UtilityFunctionsParam.AffectorsCompareTargetsEvery(
        ref affectors,
        ref targets,
        UtilityFunctionsParam.GetCurrentValFloat,
        myDelegate,
        CalculationLogic
        );
    }

    private ValueWorker GetValueByEnum()
    {
        switch (DamageResistanceType)
        {
            case DamageResistanceType.Mitigation:
                return UtilityFunctionsParam.GetMitigationValueMod;
            case DamageResistanceType.Trashhold:
                return UtilityFunctionsParam.GetTrashholdValueMod;
            default:
                throw new ArgumentException($"Unsupported value {nameof(DamageResistanceType)}");
        }
    }
    #endregion
}
