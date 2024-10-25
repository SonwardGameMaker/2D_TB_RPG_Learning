using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BiteAttack : BehaviourScriptBase, IAttackable, IApCosted
{
    private CharacterInfo _characterInfo;
    private Animator _animator;
    private ApMpSystem _apMpSystem;

    [SerializeField] private int _apCost;

    #region init
    public BiteAttack() { }
    public BiteAttack(CharacterBlank character, Animator animator) : base(character)
    {
        _animator = animator;
        _characterInfo = character.GetComponent<CharacterInfo>();
        _apMpSystem = character.ApMpSystem;
    }

    protected override void SetActionName()
        => _name = typeof(BiteAttack).Name;

    private void Awake()
    {
        _name = typeof(BiteAttack).Name;
    }

    public void Setup(CharacterInfo characterInfo, Animator animator)
    {
        // Add ApCost for attacks
        Setup(characterInfo.GetComponent<CharacterBlank>(), 0);
        _apMpSystem = _character.ApMpSystem;

        _characterInfo = characterInfo;
        _animator = animator;
    }
    #endregion

    #region properties
    public int ApCost { get => _apCost; }
    #endregion

    public void Attack(IDamagable target, Action<bool, string> onEndCorutineAction)
    {
        throw new NotImplementedException();
    }

    #region internal operations
    public override bool CheckIfEnoughtResources()
    {
        throw new System.NotImplementedException();
    }

    public override void ConsumeResources()
    {
        throw new System.NotImplementedException();
    }

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
    private IEnumerator AttackCoroutine(IDamagable target, Animator animator, Action<bool, string> action)
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
