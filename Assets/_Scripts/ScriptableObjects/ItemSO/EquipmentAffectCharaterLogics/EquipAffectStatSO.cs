using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ValueType { MaxValue, MinValue, CurrentValue, TrashholdValue, MitigationValues }

[CreateAssetMenu(menuName = "Scriptables/Items/Affection/EquipOnStat")]
internal class EquipAffectStatSO : EquipAffectCharBaseSO
{
    public StatType StatType;
    public float AffectionValue;
    //public ValueType ValueType;

    public override ParInteraction AffectCharacter(CharacterBlank character)
    {
        return new ParInteraction(new FlatParameter($"Item affet {StatType.ToString()}", AffectionValue),
            (Stat)character.Stats.GetType().GetProperty(StatType.ToString()).GetValue(character.Stats),
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
