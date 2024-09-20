using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour, IAttackable
{
    private CharacterInfo _characterInfo;

    private Coroutine _coroutine;

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
