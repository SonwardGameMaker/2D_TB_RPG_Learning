using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResourceAffectorCreator : ParInteractionCreator
{
    public CharResourceType CharResourceType;

    #region init
    public ResourceAffectorCreator() { }
    
    public ResourceAffectorCreator(float modifierAmount, ModifierType modifierType, CharResourceType charResourceType) : base(modifierAmount, modifierType)
    {
        CharResourceType = charResourceType;
    }

    public ResourceAffectorCreator(ResourceAffectorCreator other) : this(other.ModifierAmount, other.ModifierType, other.CharResourceType) { }
    #endregion

    #region external interactions
    public override ParInteraction CreateParInteraction(CharacterBlank character)
        => GetInteractionByEnum(character);
    #endregion

    #region internal operations
    protected override (float, ModifierType) CalculationLogic(CharParameterBase affector)
        => new(ModifierAmount, ModifierType);

    private ParInteraction GetInteractionByEnum(CharacterBlank character)
    {
        switch (CharResourceType)
        {
            case CharResourceType.Health:
                return character.Health.CreateHealthPointsEffect(ItemAffectsResource());
            case CharResourceType.ActionPoints:
                return character.ApMpSystem.CreateApEffect(ItemAffectsResource());
            case CharResourceType.MovementPoints:
                return character.ApMpSystem.CreateMpEffect(ItemAffectsResource());
            default:
                throw new ArgumentException($"Unsupported value {nameof(CharResourceType)}");
        }
    }

    private void ItemAffectResourceLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
    => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
        ref affectors,
        ref targets,
        UtilityFunctionsParam.GetCurrentValFloat,
        UtilityFunctionsParam.GetMaxValueMod,
        CalculationLogic
        );

    private (List<CharParameterBase>, ModValueCalculateLogic) ItemAffectsResource()
        => (new List<CharParameterBase>() { CreateFlatParemFromFloat() }, ItemAffectResourceLogic);
    #endregion
}
