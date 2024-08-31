using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[Serializable]
public class CharHealthSystem : ICharResourseFieldGettable
{
    private const string DEFAULT_NAME = "Health";
    private const float DEFAULT_MIN_VALUE = 0.0f;
    private const float DEFAULT_MAX_VALUE = 100.0f;

    private CharResource _health;
    //private FlatParameter _hpBonusPerLevel;

    public bool IsAlive { get; set; }

    private bool _isHealthFull;

    #region constructors and destructor
    public CharHealthSystem()
    {
        _health = new CharResource(DEFAULT_NAME, DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE);
        _health.MinValChanged += HealthValCahgeHanddler;
        _health.CurrentValChanged += HealthValCahgeHanddler;
        _health.MaxValChanged += HealthMaxValChangedHandler;

        IsAlive = true;
        _isHealthFull = IsHealthFull();
    }
    ~CharHealthSystem()
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
    public CharResourseInfo HealthInfo { get => new CharResourseInfo(_health); }
    #endregion

    #region extrnal interation
    public CharResource GetFieldByEnum(CharResourceFieldType fieldType)
    {
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

    // Health Points
    public ParInteraction CreateHealthPointsEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _health, CalculateLogic);
    public ParInteraction CreateHealthPointsEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
        => CreateHealthPointsEffect(new List<CharParameterBase> { affector }, CalculateLogic);
    public ParInteraction CreateHealthPointsEffect((List<CharParameterBase>, ModValueCalculateLogic) parameters)
        => new ParInteraction(parameters.Item1, _health, parameters.Item2);

    // Hp Bonus Per Level
    //public ParInteraction CreateHpBonusPLevelEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
    //    => new ParInteraction(affectors, _hpBonusPerLevel, CalculateLogic);
    //public ParInteraction CreateHpBonusPLevelEffect(CharParameterBase affector, ModValueCalculateLogic CalculateLogic)
    //    => CreateHpBonusPLevelEffect(new List<CharParameterBase> { affector }, CalculateLogic);
    //public ParInteraction CreateHpBonusPLevelEffect((List<CharParameterBase>, ModValueCalculateLogic) parameters)
    //    => new ParInteraction(parameters.Item1, _hpBonusPerLevel, parameters.Item2);
    #endregion

    public event Action HealthChanged;
    public event Action CharDeath;

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
    #endregion
}
