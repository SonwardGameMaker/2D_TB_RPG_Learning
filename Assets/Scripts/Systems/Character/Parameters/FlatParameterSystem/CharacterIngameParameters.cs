using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CharacterIngameParameters
{
    private const float DEFAULT_CHARACTER_RSISTANCE_VALUE = 0.0f;

    public List<DamageResistance> _damageResistances;
    private FlatParameter _meleeDamageIncreaseCoef; // percentage, scaling from Strength
    private FlatParameter _lightMeleeCriticalChanceIncreaceCoef; // percentage, scaling from Dexterity
    private FlatParameter _firearmsCriticalChanceIncreaceCoef; // percentage, scaling from Perception

    public CharacterIngameParameters()
    {
        _damageResistances = new List<DamageResistance>();
        foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
        {
            _damageResistances.Add(new DamageResistance(damageType));
        }
        _meleeDamageIncreaseCoef = new FlatParameter("Melee Damage Increasing Coeficient", DEFAULT_CHARACTER_RSISTANCE_VALUE);
        _lightMeleeCriticalChanceIncreaceCoef = new FlatParameter("Melee Critical Chance Increasing Coeficient", DEFAULT_CHARACTER_RSISTANCE_VALUE);
        _firearmsCriticalChanceIncreaceCoef = new FlatParameter("Firearm Critical Chanc Increasing Coeficient", DEFAULT_CHARACTER_RSISTANCE_VALUE);
    }

    #region properties
    public ReadOnlyCollection<DamageResistance> DamageResistances
    {
        get => new ReadOnlyCollection<DamageResistance>(_damageResistances);
    }
    public FlatParameter MeleeDamageIncreaseCoef { get =>  _meleeDamageIncreaseCoef; }
    public FlatParameter LigthMeleeCriticalChanceIncreaceCoef { get =>  _firearmsCriticalChanceIncreaceCoef; }
    public FlatParameter FirearmsCriticalChanceIncreaceCoef { get => _firearmsCriticalChanceIncreaceCoef; }
    #endregion

    #region extrnal interation
    public DamageResistance GetDamageResistanceByType(DamageType type) 
        => _damageResistances.FirstOrDefault(dr => dr.DamageType == type);

    // Resistances
    public ParInteraction CreateDamageResistanceEffect(DamageType type, List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _damageResistances.First(dt=>dt.DamageType == type), CalculateLogic);
    public ParInteraction CreateDamageResistanceEffect(DamageType type, CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(new List<CharParameterBase> { affector }, _damageResistances.First(dt => dt.DamageType == type), CalculateLogic);
    // Melee damage
    public ParInteraction CreateMeleeDamageCoefEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _meleeDamageIncreaseCoef, CalculateLogic);
    public ParInteraction CreateMeleeDamageCoefEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(new List<CharParameterBase> { affector }, _meleeDamageIncreaseCoef, CalculateLogic);
    // Melee crit
    public ParInteraction CreateLightMeleeCriticalChanceCoefEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _lightMeleeCriticalChanceIncreaceCoef, CalculateLogic);
    public ParInteraction CreateLightMeleeCriticalChanceCoefEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(new List<CharParameterBase> { affector }, _lightMeleeCriticalChanceIncreaceCoef, CalculateLogic);
    // Firearm crit
    public ParInteraction CreateFirearmCriticalChanceCoefEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _firearmsCriticalChanceIncreaceCoef, CalculateLogic);
    public ParInteraction CreateFirearmCriticalChanceCoefEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(new List<CharParameterBase> { affector }, _firearmsCriticalChanceIncreaceCoef, CalculateLogic);
    #endregion
}
