using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal abstract class Attackable : BehaviourScriptBase, IAttackable, IApCosted, IGridManagerInteractabe
{
    protected CharacterInfo _characterInfo;
    protected Animator _animator;
    protected ApMpSystem _apMpSystem;
    protected GridManager _gridManager;

    [SerializeField] protected AttackRangeType _attackRangeType;
    [SerializeField] protected FlatParameter _apCost = new FlatParameter("ApCost", 0);
    [SerializeField] protected int _attackRadius;

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

    public void SetupGridManager(GridManager gridManager)
    {
        _gridManager = gridManager;
    }
    #endregion

    #region properties
    public AttackRangeType AttackRangeType { get => _attackRangeType; }
    public int ApCost { get => (int)_apCost.CurrentValue; }
    public int AttackRadius { get => _attackRadius; }
    #endregion

    #region external interactions
    public virtual void Attack(IDamagable target, Action<bool, string> onEndCorutineAction)
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

    public override bool CheckIfEnoughtResources()
        => _apMpSystem.ActionPoints.CurrentValue >= _apCost.CurrentValue;

    public override void ConsumeResources()
        => _apMpSystem.TryChangeCurrAp(-_apCost.CurrentValue);

    public override bool TryConsumeResources()
    {
        if (CheckIfEnoughtResources())
        {
            ConsumeResources();
            return true;
        }
        return false;
    }

    public override PlayerBehaviourScriptContainer GenerateScriptContainer()
        => new AttackableContainer(this);

    public ParInteraction CreateApCostEffect(List<CharParameterBase> affectors, ModValueCalculateLogic CalculateLogic)
        => new ParInteraction(affectors, _apCost, CalculateLogic);
    #endregion

    #region internal operations
    protected void TryHit(IDamagable target)
    {
        HitDataContainer hitData = _characterInfo.CalculateHitData();

        target.TakeHit(hitData);
    }

    protected bool IsAnimationFinished(Animator animator, string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f;
    }

    protected float GetAnimationLength(Animator animator, string animationName)
        => animator.runtimeAnimatorController.animationClips.First(anim => anim.name == animationName).length;
    #endregion

    #region coroutines
    private IEnumerator AttackCoroutine(IDamagable target, Animator animator, Action<bool,string> action)
    {
        animator.Play(AnimationConstants.PreparingToShootPistol);
        yield return new WaitForSeconds(GetAnimationLength(animator, AnimationConstants.PreparingToShootPistol));

        animator.Play(AnimationConstants.PistolShooting);
        yield return new WaitForSeconds(GetAnimationLength(animator, AnimationConstants.PistolShooting));
        TryHit(target);

        animator.Play(AnimationConstants.HidingPistol);
        yield return new WaitForSeconds(GetAnimationLength(animator, AnimationConstants.HidingPistol));

        action?.Invoke(true, SUCCESSFUL_EXECUTION_MEASSAGE);
        yield break;
    }
    #endregion
}
