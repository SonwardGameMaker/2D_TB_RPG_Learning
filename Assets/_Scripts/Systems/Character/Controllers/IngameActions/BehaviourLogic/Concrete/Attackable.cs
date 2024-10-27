using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Attackable : BehaviourScriptBase, IAttackable, IApCosted
{
    private CharacterInfo _characterInfo;
    private Animator _animator;
    private ApMpSystem _apMpSystem;

    [SerializeField] private int _apCost;

    #region init
    protected override void SetActionName()
        => _name = typeof(Attackable).Name;

    private void Awake()
    {
        _name = typeof(Attackable).Name;
    }

    public void Setup(CharacterInfo characterInfo, Animator animator)
    {
        // Add ApCost for attacks
        Setup(characterInfo.GetComponent<CharacterBlank>());
        _apMpSystem = _character.ApMpSystem;

        _characterInfo = characterInfo; 
        _animator = animator;
    }

    public override void Setup(CharacterBlank character)
    {
        base.Setup(character);
        _characterInfo = character.GetComponent<CharacterInfo>();
        _animator = character.GetComponentInChildren<Animator>();
        _apMpSystem = character.ApMpSystem;
    }
    #endregion

    #region properties
    public int ApCost { get => _apCost; }
    #endregion

    #region external interactions
    public void Attack(IDamagable target, Action<bool, string> onEndCorutineAction)
    {
        if (TryConsumeResources())
        {
            _coroutine = StartCoroutine(AttackCoroutine(target, _animator, onEndCorutineAction));
        }
        else
        {
            onEndCorutineAction?.Invoke(false, NOT_ENOUGHT_AP_MEASSAGE);
        }
    }
    #endregion

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

    public override bool CheckIfEnoughtResources()
        => _apMpSystem.ActionPoints.CurrentValue >= _apCost;

    public override void ConsumeResources()
        => _apMpSystem.TryChangeCurrAp(-_apCost);

    public override bool TryConsumeResources()
    {
        if (CheckIfEnoughtResources())
        {
            ConsumeResources();
            return true;
        }
        return false;
    }
    #endregion

    #region coroutines
    private IEnumerator AttackCoroutine(IDamagable target, Animator animator, Action<bool,string> action)
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

        action?.Invoke(true, SUCCESSFUL_EXECUTION_MEASSAGE);
        yield break;
    }
    #endregion
}
