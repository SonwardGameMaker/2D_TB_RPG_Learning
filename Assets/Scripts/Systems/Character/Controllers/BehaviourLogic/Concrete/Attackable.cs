using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : BaseControllerLogic, IAttackable
{
    private CharacterInfo _characterInfo;
    private Animator _animator;

    public void Setup(CharacterInfo characterInfo, Animator animator)
    {
        _characterInfo = characterInfo; 
        _animator = animator;
    }

    public (bool, string) Attack(IDamagable target, Action onEndCorutineAction)
    {
        _coroutine = StartCoroutine(AttackCoroutine(target, _animator, onEndCorutineAction));
        // TODO make rotation to consume movement points
        return (true, "Attack was successful");
    }

    #region internal operations
    private void TryHit(IDamagable target)
    {
        HitDataContainer hitData = _characterInfo.CalculateHitData();

        target.TakeHit(hitData);
    }

    private bool IsAnimationFinished(Animator animator, string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f;
    }
    #endregion

    #region coroutines
    private IEnumerator AttackCoroutine(IDamagable target, Animator animator, Action action)
    {
        animator.Play(AnimationConstants.PreparingToShootPistol);
        yield return new WaitUntil(() => IsAnimationFinished(animator, AnimationConstants.PreparingToShootPistol));

        animator.SetBool(AnimationConstants.ShootingBool, true);
        yield return null;
        TryHit(target);
        yield return null;
        animator.SetBool(AnimationConstants.ShootingBool, false);

        animator.Play(AnimationConstants.HidingPistol);
        yield return new WaitUntil(() => IsAnimationFinished(animator, AnimationConstants.HidingPistol));

        action?.Invoke();
        yield break;
    }
    #endregion
}
