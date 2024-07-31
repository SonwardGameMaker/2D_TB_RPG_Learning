using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum ValueType { MaxValue, MinValue, CurrentValue, TrashholdValue, MitigationValues }

[CreateAssetMenu(menuName = "Scriptables/Items/Affection/EquipOnStat")]
public class EquipAffectStatSO : EquipAffectCharBaseSO
{
    public StatType StatType;
    public float AffectionValue;
    public ValueType ValueType;

    public override ParInteraction AffectCharacter(CharacterBlank character)
    {
        return new ParInteraction(new FlatParameter("Item effet", AffectionValue),
            (Stat)character.Stats.GetType().GetProperty(StatType.ToString()).GetValue(character.Stats));
    }

    private void ItemAffect(ref List<CharParameterBase> affectors, ref List<CharParameterBase> tarets)
        => UtilityFunctionsParam.AffectorsCompareTargetsEvery(
            ref affectors,
            ref tarets, 
            UtilityFunctionsParam.GetCurrentValFloat,
            new CurrentValueWorker() );
}
