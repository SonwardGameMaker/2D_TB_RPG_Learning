using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharHealth : CharResource
{
    private new const float DEFAULT_MIN_VALUE = 0.0f;
    private new const float DEFAULT_MAX_VALUE = 100.0f;
    public bool IsAlive { get; set; }

    private bool _isHealthFull;

    public CharHealth() : base(DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE)
    {
        IsAlive = true;
        _isHealthFull = IsHealthFull();
    }
    public void SetUp()
    {
        // Create initialization by Scriptable Object
    }

    #region extrnal_interation
    public void ChangeHp(float amount)
    {
        CurrentValue += amount;
        if (CurrentValue < MinValue)
            Death();
        _isHealthFull = IsHealthFull();

    }
    public void Death()
    {
        IsAlive = false;
        CharDeath?.Invoke();
    }

    public Action CharDeath;

    public void LevelConstAffectHp(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets)
        => UtilityFunctionsParam.ParamModifyingValueSimultaneously(
            ref affectors,
            ref targets, 
            UtilityFunctionsParam.GetCurrentValFloat,
            UtilityFunctionsParam.GetMaxVal,
            LevelConstAffectionLogic
            );
    #endregion

    private bool IsHealthFull()
    {
        return CurrentValue == MaxValue.RealValue;
    }
    protected override void HandleMaxValEvents()
    {
        if (_isHealthFull)
            CurrentValue = MaxValue.RealValue;
        base.HandleMaxValEvents();
    }
    private List<Modifier> LevelConstAffectionLogic(List<CharParameterBase> affectors)
    {
        float constMod = UtilityFunctionsParam.GetCurrentValFloat(affectors[1]) - 5.0f;
        float ConstModFirstLvl = constMod * 4.0f;
        float result = ConstModFirstLvl;
        for (int i = 2; i <= UtilityFunctionsParam.GetCurrentValFloat(affectors[0]); i++)
            result += constMod + 5.0f;

        return new List<Modifier> { new Modifier(result, ModifierType.Flat, affectors[1])};
    }
}
