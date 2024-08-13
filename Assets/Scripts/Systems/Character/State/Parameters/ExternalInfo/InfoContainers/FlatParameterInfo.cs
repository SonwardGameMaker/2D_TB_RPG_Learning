using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatParameterInfo : CharacterDataBaseInfo
{
    private FlatParameter _flatParameter;

    public FlatParameterInfo(FlatParameter flatParameter)
    {
        _flatParameter = flatParameter;
    }

    #region properties
    public override string Name { get => _flatParameter.Name; }

    public float CurrentValueBase { get => _flatParameter.CurrentValueBase; }
    public float CurrentValue { get => _flatParameter.CurrentValue; }
    #endregion

    #region events subscription
    public void CurrentValueSubscribe(Action subscription) => _flatParameter.CurrentValChanged += subscription;

    public void CurrentValueUnsubscribe(Action subscription) => _flatParameter.CurrentValChanged -= subscription;

    #endregion
}
