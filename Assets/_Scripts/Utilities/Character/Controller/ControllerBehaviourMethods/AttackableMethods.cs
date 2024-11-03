using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AttackableMethods
{
    #region internal operations
    private static bool IsAnimationFinished(Animator animator, string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f;
    }

    private static float GetAnimationLength(Animator animator, string animationName)
        => animator.runtimeAnimatorController.animationClips.First(anim => anim.name == animationName).length;
    #endregion

    #region coroutines
    /// <summary>
    /// Basic coroutine for Attackable class
    /// </summary>
    /// <param name="target"></param>
    /// <param name="animator"></param>
    /// <param name="preparingAnimationName">If this value is null or "" corresponding animation will be skipped</param>
    /// <param name="attackAnimationName">If this value is null or "" corresponding animation will be skipped</param>
    /// <param name="endAnimationName">If this value is null or "" corresponding animation will be skipped</param>
    /// <param name="TryHit"></param>
    /// <param name="action">Action that invokes in the end of coroutine</param>
    /// <param name="successfulExecutionMessage">Message in action if coroutine was succesfull</param>
    /// <returns></returns>
    public static IEnumerator DefaultAttackCoroutine(
        IDamagable target,
        Animator animator,
        string preparingAnimationName,
        string attackAnimationName,
        string endAnimationName,
        Action<IDamagable> TryHit,
        Action<bool, string> action,
        string successfulExecutionMessage)
    {
        if (preparingAnimationName != null && preparingAnimationName.Length != 0)
        {
            animator.Play(preparingAnimationName);
            yield return new WaitForSeconds(GetAnimationLength(animator, preparingAnimationName));
        }

        if (attackAnimationName != null && attackAnimationName.Length != 0)
        {
            animator.Play(attackAnimationName);
            yield return new WaitForSeconds(GetAnimationLength(animator, attackAnimationName));
        }

        TryHit(target);

        if (endAnimationName != null && endAnimationName.Length != 0)
        {
            animator.Play(endAnimationName);
            yield return new WaitForSeconds(GetAnimationLength(animator, endAnimationName));
        }
        
        action?.Invoke(true, successfulExecutionMessage);
        yield break;
    }
    #endregion
}
