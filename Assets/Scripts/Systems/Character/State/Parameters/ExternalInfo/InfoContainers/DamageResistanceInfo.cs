using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageResistanceInfo : CharacterDataBaseInfo
{
    private DamageResistance _damageResistance;

    public DamageResistanceInfo(DamageResistance damageResistance)
    {
        _damageResistance = damageResistance;
    }

    #region properties
    public override string Name { get => _damageResistance.Name; }

    public float TrashholdValueBase { get => _damageResistance.TrashholdBase; }
    public float TrashholdValue { get => _damageResistance.Trashhold; }

    public float MitigationValueBase { get => _damageResistance.MitigationBase; }
    public float MitigationValue { get => _damageResistance.Mitigation; }
    #endregion

    #region events subscription
    public void TrashholdValueSubscribe(Action subscription) => _damageResistance.TrashholdChanged += subscription;
    public void MitigationValueSubscribe(Action subscription) => _damageResistance.MitigationChanged += subscription;
    public void SubscribeToAll(Action subscription)
    {
        _damageResistance.TrashholdChanged += subscription;
        _damageResistance.MitigationChanged += subscription;
    }

    public void TrashholdValueUnsubscribe(Action subscription) => _damageResistance.TrashholdChanged -= subscription;
    public void MitigationValueUnsubscribe(Action subscription) => _damageResistance.MitigationChanged -= subscription;
    public void UnsubscribeToAll(Action subscription)
    {
        _damageResistance.TrashholdChanged -= subscription;
        _damageResistance.MitigationChanged -= subscription;
    }
    #endregion
}
