using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BiteAttack : BehaviourScriptBase, IAttackable, IApCosted
{
    private CharacterInfo _characterInfo;
    private Animator _animator;
    private ApMpSystem _apMpSystem;
    private IDamagable _selfDamagable;

    [SerializeField] private int _apCost;

    #region init
    protected override void SetActionName()
        => _name = typeof(BiteAttack).Name;

    private void Awake()
    {
        _name = typeof(BiteAttack).Name;
    }

    public void Setup(CharacterInfo characterInfo, Animator animator)
    {
        Setup(characterInfo.GetComponent<CharacterBlank>());
        _apMpSystem = _character.ApMpSystem;
        _selfDamagable = characterInfo.GetComponentInChildren<IDamagable>();

        _characterInfo = characterInfo;
        _animator = animator;
    }

    public override void Setup(CharacterBlank character)
    {
        base.Setup(character);
        _characterInfo = character.GetComponent<CharacterInfo>();
        _selfDamagable = character.GetComponentInChildren<IDamagable>();
        _animator = character.GetComponentInChildren<Animator>();
        _apMpSystem = character.ApMpSystem;
    }
    #endregion

    #region properties
    public int ApCost { get => _apCost; }
    #endregion

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

    #region internal operations
    public override bool CheckIfEnoughtResources()
         => _apMpSystem.ActionPoints.CurrentValue >= _apCost;

    public override void ConsumeResources()
        => _apMpSystem.TryChangeCurrAp(-_apCost);

    private void TryHit(IDamagable target)
    {
        if (target.TakeHit(new HitDataContainer(_characterInfo, 15, DamageType.Mechanical, _character.Stats.Melee.CurrentValue)))
        {
            Debug.Log("Healing");
            _selfDamagable.TakeHealing(15);
        } 
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
