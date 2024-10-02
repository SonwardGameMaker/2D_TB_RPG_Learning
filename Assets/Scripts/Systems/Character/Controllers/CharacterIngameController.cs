using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    private CharacterBlank _character;
    protected CharacterInfo _characterInfo;
    protected Coroutine _coroutine;
    protected Animator _animator;

    private ControllerStates _controllerState;

    private IMovable _movable;
    private IAttackable _attackable;

    private ActionCommandList _commandList;

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

        _commandList = new ActionCommandList();
    }

    #region properties
    //internal CharacterStateMachine CharacterStateMachine { get => _stateMachine; }
    public ControllerStates ControllerState { get => _controllerState; }
    #endregion

    #region external interactions
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
            throw new Exception("Character still busy");
        }

        SetBusyState();
        (bool, string) result = _movable.Move(path, SetIdleState);

        if (result.Item1 == false)
        {
            //TODO maybe it's need to be an error throw
            Debug.Log(result.Item2);
        }
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
        (bool, string) result = _movable.Move(path, () => { });
        if (result.Item1 == false)
        {
            //TODO maybe it's need to be an error throw
            Debug.Log(result.Item2);
            SetIdleState();
            return;
        }
        result = _movable.Rotate(targetPosition, () => { });
        if (result.Item1 == false)
        {
            //TODO maybe it's need to be an error throw
            Debug.Log(result.Item2);
            SetIdleState();
            return;
        }
        result = _attackable.Attack(target, SetIdleState);
        if (result.Item1 == false)
        {
            //TODO maybe it's need to be an error throw
            Debug.Log(result.Item2);
            SetIdleState();
            return;
        }
    }
    #endregion

    #region internal oprations
    private void SetIdleState()
    {
        _controllerState = ControllerStates.Idle;
    }
    private void SetBusyState()
    {
        _controllerState = ControllerStates.Busy;
    }
    #endregion
}
