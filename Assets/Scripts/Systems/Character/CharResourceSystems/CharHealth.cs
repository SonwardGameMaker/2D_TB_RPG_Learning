using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharHealth
{
    private const float DEFAULT_MIN_VALUE = 0.0f;
    private const float DEFAULT_MAX_VALUE = 100.0f;
    private const string DEFAULT_NAME = "Health";

    private CharResource _health;

    public bool IsAlive { get; set; }

    private bool _isHealthFull;

    public CharHealth()
    {
        _health = new CharResource(DEFAULT_NAME, DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE);
        _health._isCurrValCanReachBelowMinVal = true;
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

    #region extrnal_interation
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

    public ParInteraction LevelConstAffectHp(Stat level, Stat constitution)
        => new ParInteraction(new List<CharParameterBase> { level, constitution}, _health, LevelConstAffectHelath);
    #endregion

    public event Action HealthChanged;

    private bool IsHealthFull()
    {
        return _health.CurrentValue >= _health.MaxValue;
    }
    private void HealthValCahgeHanddler()
    {
        HealthChanged?.Invoke();
    }
    private void HealthMaxValChangedHandler()
    {
        _isHealthFull = IsHealthFull();
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
}
