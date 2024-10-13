using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharResourceInfo : CharacterDataBaseInfo
{
    private CharResource _resource;

    public CharResourceInfo(CharResource resource)
    {
        _resource = resource;
    }

    #region properties
    public override string Name { get =>  _resource.Name; }

    public float MaxValueBase { get => _resource.MaxValueBase; }
    public float MaxValue { get => _resource.MaxValue; }

    public float MinValue { get => _resource.MinValue; }

    public float CurrentValue { get => _resource.CurrentValue; }
    #endregion

    #region events subscription
    public void MinValueSubscribe(Action subscription) => _resource.MinValChanged += subscription;
    public void CurrentValueSubscribe(Action subscription) => _resource.CurrentValChanged += subscription;
    public void MaxValueSubscribe(Action subscription) => _resource.MaxValChanged += subscription;
    public void SubscribeToAll(Action subscription)
    {
        _resource.MaxValChanged += subscription;
        _resource.MinValChanged += subscription;
        _resource.CurrentValChanged += subscription;
    }

    public void MinValueUnsubscribe(Action subscription) => _resource.MinValChanged -= subscription;
    public void CurrentValueUnsubscribe(Action subscription) => _resource.CurrentValChanged -= subscription;
    public void MaxValueUnsubscribe(Action subscription) => _resource.MaxValChanged -= subscription;
    public void UnsubscribeToAll(Action subscription)
    {
        _resource.MaxValChanged -= subscription;
        _resource.MinValChanged -= subscription;
        _resource.CurrentValChanged -= subscription;
    }
    #endregion
}
