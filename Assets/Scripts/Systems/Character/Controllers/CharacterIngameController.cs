using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    protected CharacterInfo _characterInfo;
    protected Coroutine _coroutine;
    protected Animator _animator;

    private ControllerStates _controllerState;

    private IMovable _movable;
    private IAttackable _attackable;

    // State machine -------------------------------------------------------------------------------------------
    //private CharacterStateMachine _stateMachine;

    //private CharacterIdleState _idleState;

    //private void Awake()
    //{


    //    _stateMachine = new CharacterStateMachine();

    //    _idleState = new CharacterIdleState(GetComponent<CharacterBlank>(), this, _stateMachine);
    //}
    // ---------------------------------------------------------------------------------------------------------

    public void Start()
    {
        _characterInfo = GetComponent<CharacterInfo>();
        _animator = GetComponentInChildren<Animator>();

        _controllerState = ControllerStates.Idle;

        //_stateMachine.Setup(_idleState);

        _movable = GetComponentInChildren<IMovable>();
        _attackable = GetComponentInChildren<IAttackable>();

        _movable.Setup(_characterInfo, _animator);
        _attackable.Setup(_characterInfo, _animator);
    }

    #region properties
    //internal CharacterStateMachine CharacterStateMachine { get => _stateMachine; }
    #endregion

    public void Rotate(Vector3 targetPosition)
    {
        if (_controllerState == ControllerStates.Busy) return;

        SetBusyState();
        _movable.Rotate(targetPosition, SetIdleState);
    }

    public void Walk(List<PathfinderNodeBase> path)
    {
        if (path == null) throw new Exception("Path is null");
        if (_controllerState == ControllerStates.Busy)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        SetBusyState();
        List<TileNode> nodePath = path.Select(pnb => pnb.TargetNode).ToList();
        _movable.Move(nodePath, SetIdleState);
    }

    public void Attack(IDamagable target)
    {
        if (_controllerState == ControllerStates.Busy)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        SetBusyState();
        _attackable.Attack(target, SetIdleState);
    }

    public void WalkAndAttack(List<PathfinderNodeBase> path, IDamagable target, Vector3 targetPosition)
    {
        if (path == null) throw new Exception("Path is null");
        if (_controllerState == ControllerStates.Busy)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        SetBusyState();
        List<TileNode> nodePath = path.Select(pnb => pnb.TargetNode).ToList();
        if (nodePath == null) throw new Exception("NodePath is null");
        _movable.Move(nodePath, 
            () => _movable.Rotate(targetPosition,
            () => _attackable.Attack(target, SetIdleState))
            );
    }

    private void SetIdleState()
    {
        _controllerState = ControllerStates.Idle;
    }
    private void SetBusyState()
    {
        _controllerState = ControllerStates.Busy;
    }
}
