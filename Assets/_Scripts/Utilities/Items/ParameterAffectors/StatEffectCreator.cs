using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatEffectCreator : ParInteractionCreator
{
    public StatType StatType;

    #region init
    public StatEffectCreator() { }

    public StatEffectCreator(float modifierAmount, ModifierType modifierType, StatType statType) : base(modifierAmount, modifierType)
    {
        StatType = statType;
    }

    public StatEffectCreator(StatEffectCreator other) : this(other.ModifierAmount, other.ModifierType, other.StatType) { }
    #endregion

    #region external interactions
    public override ParInteraction CreateParInteraction(CharacterBlank character)
        => new ParInteraction(CreateFlatParemFromFloat(), GetStatByEnum(character.Stats), AffectionOnStat);
    #endregion

    #region internal operactions
    private void AffectionOnStat(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targetStats)
    {
        UtilityFunctionsParam.AffectorsCompareTargetsEvery(
            ref affectors,
            ref targetStats,
            UtilityFunctionsParam.GetCurrentValFloat,
            UtilityFunctionsParam.GetCurrValueMod,
            CalculationLogic);
    }

    protected override (float, ModifierType) CalculationLogic(CharParameterBase affector)
        => new(ModifierAmount, ModifierType);

    private Stat GetStatByEnum(CharacterStatsSystem stats)
    {
        switch (StatType)
        {
            case StatType.Level:
                return stats.Level;
            case StatType.Strength:
                return stats.Strength;
            case StatType.Dexterity:
                return stats.Dexterity;
            case StatType.Agility:
                return stats.Agility;
            case StatType.Constitution:
                return stats.Constitution;
            case StatType.Perception:
                return stats.Perception;
            case StatType.Charisma:
                return stats.Charisma;
            case StatType.Intelligence:
                return stats.Intelligence;
            case StatType.LightFirearm:
                return stats.LightFirearm;
            case StatType.Firearm:
                return stats.Firearm;
            case StatType.Melee:
                return stats.Melee;
            case StatType.HeavyMelee:
                return stats.HeavyMelee;
            case StatType.Dodge:
                return stats.Dodge;
            case StatType.Stealth:
                return stats.Stealth;
            case StatType.Hacking:
                return stats.Hacking;
            case StatType.Lockpicking:
                return stats.Lockpicking;
            case StatType.Pickpocketing:
                return stats.Pickpocketing;
            case StatType.Persuasion:
                return stats.Persuasion;
            case StatType.Intimidation:
                return stats.Intimidation;
            case StatType.Mercantile:
                return stats.Mercantile;
            default:
                throw new ArgumentException($"Unsupported {nameof(StatType)}");
        }
    }
    #endregion
}
