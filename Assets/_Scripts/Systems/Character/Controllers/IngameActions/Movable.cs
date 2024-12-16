using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

internal class Movable : BehaviourScriptBase, IMovable
{
    private CharacterInfo _characterInfo;
    private ApMpSystem _apMpSystem;
    private Animator _animator;
    private List<PathfinderNodeBase> _path;

    [SerializeField] private float _speed;
    [SerializeField] private float _mpApCostModifier;

    #region init
    protected override void SetActionName()
        => _name = typeof(Movable).Name;

    public void Setup(CharacterInfo characterInfo, Animator animator)
    {
        base.Setup(characterInfo.GetComponent<CharacterBlank>());

        _characterInfo = characterInfo; 
        _apMpSystem = _character.ApMpSystem;
        _animator = animator;

        _path = null;
    }
    #endregion

    #region properties
    public float Speed { get => _speed; set => _speed = value; }
    public float MpApCostModifier { get => _mpApCostModifier; }
    #endregion

    #region external interactions
    public void Move(List<PathfinderNodeBase> path, Action<bool, string> onEndCoroutineAction)
    {
        _path = path;

        if (TryConsumeResources())
        {
            _coroutine = StartCoroutine(MovePathCoroutine(path.Select(pn => pn.TargetNode).ToList(), 
                _animator, onEndCoroutineAction));
        }
        else
        {
            // TODO make path shorter for avaliable mp&ap
            onEndCoroutineAction?.Invoke(false, NOT_ENOUGHT_AP_MEASSAGE);
        }
    }

    public void Rotate(Vector3 targetPosition, Action<bool, string> onEndCoroutineAction)
    {
        _coroutine = StartCoroutine(ChangeRotationCorutine(targetPosition, onEndCoroutineAction));
    }

    public bool CheckIfEnoughtResources(List<PathfinderNodeBase> path)
        => _apMpSystem.MovementPoints.CurrentValue + _apMpSystem.ActionPoints.CurrentValue >= CalculatePathCost(path);
    public override bool CheckIfEnoughtResources()
    {
        if (_path == null) return false;

        return CheckIfEnoughtResources(_path);
    }

    public override void ConsumeResources()
    {
        _apMpSystem.TryChangeCurrMp(-CalculatePathCost(_path));
    }

    public override bool TryConsumeResources()
    {
        if (CheckIfEnoughtResources())
        {
            ConsumeResources();
            return true;
        }
        else
            return false;
    }

    public override PlayerBehaviourScriptContainer GenerateScriptContainer()
        => new MovableContainer(this);
    #endregion

    #region internal operations
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.parent.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private int CalculatePathCost(List<PathfinderNodeBase> path)
        => UtilityFunctionsControllers.CalculatePathCost(path, _mpApCostModifier);
    #endregion

    #region coroutines
    private IEnumerator MovePathCoroutine(List<TileNode> path, Animator animator, Action<bool, string> action)
    {
        float step = Speed * Time.fixedDeltaTime;
        CharacterInfo character = _characterInfo;
        if (character == null) throw new ArgumentNullException("Character");

        animator.SetBool(AnimationConstants.WalkingBool, true);

        for (int i = 1; i < path.Count; i++)
        {
            if (path[i].TrySetCharacter(character))
            {
                path[i - 1].TryRemoveCharacter();

                Vector3 targetPosition = path[i].WorldPositionOfCenter;
                RotateTowards(targetPosition);
                while (!character.transform.position.Equals(targetPosition))
                {
                    character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, step);
                    yield return null;
                }
            }
            else
            {
                animator.SetBool(AnimationConstants.WalkingBool, false);
                action?.Invoke(false, "Cannot put character in cell");
                _path = null;
                yield break;
            }
        }
        animator.SetBool(AnimationConstants.WalkingBool, false);
        action?.Invoke(true, SUCCESSFUL_EXECUTION_MEASSAGE);
        _path = null;
    }

    private IEnumerator ChangeRotationCorutine(Vector3 targetPosition, Action<bool, string> action)
    {
        RotateTowards(targetPosition);
        action?.Invoke(true, SUCCESSFUL_EXECUTION_MEASSAGE);
        yield return null;
    }
    #endregion
}
