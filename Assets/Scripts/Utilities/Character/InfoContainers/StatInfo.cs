using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatInfo : CharacterDataBaseInfo
{
    private Stat _stat;

    public StatInfo(Stat stat)
    {
        _stat = stat;
    }

    #region properties
    public override string Name { get => _stat.Name; }

    public float MaxValueBase { get => _stat.MaxValueBase; }
    public float MaxValue {  get => _stat.MaxValue; }

    public float MinValue { get => _stat.MinValue; }

    public float CurrentValueBase { get => _stat.CurrentValueBase; }
    public float CurrentValue { get => _stat.CurrentValue; }
    #endregion

    #region events subscription
    public void MinValueSubscribe(Action subscription) => _stat.MinValChanged += subscription;
    public void CurrentValueSubscribe(Action subscription) => _stat.CurrentValChanged += subscription;
    public void MaxValueSubscribe(Action subscription) => _stat.MaxValChanged += subscription;
    public void SubscribeToAll(Action subscription)
    {
        _stat.MaxValChanged += subscription;
        _stat.MinValChanged += subscription;
        _stat.CurrentValChanged += subscription;
    }

    public void MinValueUnsubscribe(Action subscription) => _stat.MinValChanged -= subscription;
    public void CurrentValueUnsubscribe(Action subscription) => _stat.CurrentValChanged -= subscription;
    public void MaxValueUnsubscribe(Action subscription) => _stat.MaxValChanged -= subscription;
    public void UnsubscribeToAll(Action subscription)
    {
        _stat.MaxValChanged -= subscription;
        _stat.MinValChanged -= subscription;
        _stat.CurrentValChanged -= subscription;
    }
    #endregion
}
