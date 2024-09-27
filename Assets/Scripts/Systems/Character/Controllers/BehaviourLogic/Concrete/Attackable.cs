using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : BaseControllerLogic, IAttackable
{
    private CharacterInfo _characterInfo;

    public void Setup(CharacterInfo characterInfo)
    {
        _characterInfo = characterInfo; 
    }

    public void Attack(IDamagable target, Action onEndCorutineAction)
    {
        _coroutine = StartCoroutine(AttackCoroutine(target, onEndCorutineAction));
    }

    #region internal operations
    private void TryHit(IDamagable target)
    {
        HitDataContainer hitData = _characterInfo.CalculateHitData();

        target.TakeHit(hitData);
    }
    #endregion

    #region coroutines
    private IEnumerator AttackCoroutine(IDamagable target, Action action)
    {
        TryHit(target);
        action?.Invoke();
        yield break; ;
    }
    #endregion
}
