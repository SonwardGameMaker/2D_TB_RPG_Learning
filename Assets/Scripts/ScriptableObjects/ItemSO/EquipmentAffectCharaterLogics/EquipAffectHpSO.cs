using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/Affection/EquipOnHealth")]
internal class EquipAffectHpSO : EquipAffectCharBaseSO
{
    public float AffectionValue;

    public override ParInteraction AffectCharacter(CharacterBlank character)
    {
        return character.Health.CreateHealthPointsEffect(
            new FlatParameter("Item affet Health Points", AffectionValue),
            ItemAffectionLogic);
    }

    private void ItemAffectionLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> tarets)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
            ref affectors,
            ref tarets,
            UtilityFunctionsParam.GetCurrentValFloat,
            UtilityFunctionsParam.GetCurrValueMod,
            ItemAffectModCalculating
            );

    private (float, ModifierType) ItemAffectModCalculating(CharParameterBase affector)
    {
        return new(UtilityFunctionsParam.GetCurrentValFloat(affector), ModifierType.Flat);
    }
}
