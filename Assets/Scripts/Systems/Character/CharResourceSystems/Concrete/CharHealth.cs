using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[Serializable]
public class CharHealth : ICharResourseFieldGettable
{
    private const string DEFAULT_NAME = "Health";
    private const float DEFAULT_MIN_VALUE = 0.0f;
    private const float DEFAULT_MAX_VALUE = 100.0f;

    private CharResource _health;
    private FlatParameter _hpBonusPerLevel;

    public bool IsAlive { get; set; }

    private bool _isHealthFull;

    #region constructors and destructor
    public CharHealth()
    {
        _health = new CharResource(DEFAULT_NAME, DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE);
        _health.MinValChanged += HealthValCahgeHanddler;
        _health.CurrentValChanged += HealthValCahgeHanddler;
        _health.MaxValChanged += HealthMaxValChangedHandler;

        IsAlive = true;
        _isHealthFull = IsHealthFull();
    }
    ~CharHealth()
    {
        _health.MinValChanged -= HealthValCahgeHanddler;
        _health.CurrentValChanged -= HealthValCahgeHanddler;
        _health.MaxValChanged -= HealthMaxValChangedHandler;
    }
    public void SetUp()
    {
        // Create initialization by Scriptable Object
    }
    #endregion

    #region properties
    public float MinHp
    {
        get => _health.MinValue;
        set => _health.MinValue = value;
    }
    public float CurrentHp
    {
        get => _health.CurrentValue;
        set => _health.CurrentValue = value;
    }
    public float MaxHp
    {
        get => _health.MaxValue;
    }
    #endregion

    #region extrnal interation
    public CharResource GetFieldByEnum(CharResourceFieldType fieldType)
    {
        //CharResource result = (CharResource)GetType().GetField(fieldType.ToString()).GetValue(this);
        //if (result != null) return result;
        //else throw new Exception($"Field with name {fieldType.ToString()} doesn't exist in current class");
        if (fieldType == CharResourceFieldType._health) return _health;
        throw new Exception($"Field with name {fieldType.ToString()} doesn't exist in current class");
    }

    public void ChangeHp(float amount)
    {
        _health.CurrentValue += amount;
        if (_health.CurrentValue <= _health.MinValue)
            Death();
        _isHealthFull = IsHealthFull();

    }
    public void Death()
    {
        IsAlive = false;
        CharDeath?.Invoke();
    }

    public event Action CharDeath;

    public ParInteraction CreateHealthPointsEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _health, CalculateLogic);
    public ParInteraction CreateHealthPointsEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => CreateHealthPointsEffect(new List<CharParameterBase> { affector }, CalculateLogic);
    public ParInteraction CreateHpBonusPLevelEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _hpBonusPerLevel, CalculateLogic);
    public ParInteraction CreateHpBonusPLevelEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => CreateHpBonusPLevelEffect(new List<CharParameterBase> { affector }, CalculateLogic);

    public ParInteraction LevelConstAffectHp(Stat level, Stat constitution)
        => new ParInteraction(new List<CharParameterBase> { level, constitution}, _health, LevelConstAffectHelath);
    #endregion

    public event Action HealthChanged;

    #region calculation methods
    private bool IsHealthFull()
    {
        return _health.CurrentValue == _health.MaxValue;
    }
    private void HealthValCahgeHanddler()
    {
        HealthChanged?.Invoke();
    }
    private void HealthMaxValChangedHandler()
    {
        if (_isHealthFull)
            _health.CurrentValue = _health.MaxValue;
        HealthChanged?.Invoke();
    }
    private void LevelConstAffectHelath(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
    => UtilityFunctionsParam.AffectorsAllTargetsEvery(
        ref affectors,
        ref targets,
        UtilityFunctionsParam.GetCurrentValFloat,
        UtilityFunctionsParam.GetMaxValueMod,
        LevelConstAffectingLogic
        );
    private List<Modifier> LevelConstAffectingLogic(List<CharParameterBase> affectors)
    {
        float constMod = UtilityFunctionsParam.GetCurrentValFloat(affectors[1]) - 5.0f;
        float ConstModFirstLvl = constMod * 4.0f;
        float result = ConstModFirstLvl;
        for (int i = 2; i <= UtilityFunctionsParam.GetCurrentValFloat(affectors[0]); i++)
            result += constMod + 5.0f;

        return new List<Modifier> { new Modifier(result, ModifierType.Flat, affectors[1])};
    }
    #endregion
}
