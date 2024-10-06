using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    private CharacterBlank _character;
    protected CharacterInfo _characterInfo;
    protected Animator _animator;

    private IMovable _movable;
    private IAttackable _attackable;

    private ControllerStates _controllerState;
    private ActionCommandList _commandList;


    #region events
    public event Action ExecutionComplited;
    #endregion

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
    public GridManager GridManager { get; set; }
    public ControllerStates ControllerState { get => _controllerState; }
    #endregion

    #region external interactions
    public void Rotate(Vector3 targetPosition)
    {
        if (_controllerState == ControllerStates.Busy) return;

        SetBusyState();
        //_movable.Rotate(targetPosition, SetIdleState);

        _commandList.ExecuteCommands(new RotateCommand(_movable, targetPosition), SetIdleState);
    }

    public void Walk(Vector3 targetPosition)
    {
        if (_controllerState == ControllerStates.Busy)
        {
            throw new Exception("Character still busy");
        }

        SetBusyState();
        List<ActionCommandBase> commands = new List<ActionCommandBase>();
        _commandList.ExecuteCommands(new MoveCommand(_movable, CalculatePath(targetPosition)), SetIdleState);
    }

    public void Attack(IDamagable target)
    {
        if (_controllerState == ControllerStates.Busy)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        SetBusyState();
        _commandList.ExecuteCommands(new AttackCommand(_attackable, target), SetIdleState);
    }

    public void WalkAndAttack(IDamagable target, Vector3 targetPosition, int interactDistance)
    {
        if (_controllerState == ControllerStates.Busy)
        {
            Debug.Log("Corutine still runnig");
            return;
        }

        SetBusyState();
        List<ActionCommandBase > commands = new List<ActionCommandBase>();
        commands.Add(new MoveCommand(_movable, CalculatePath(targetPosition, interactDistance)));
        commands.Add(new RotateCommand(_movable, targetPosition));
        commands.Add(new AttackCommand(_attackable, target));
        _commandList.ExecuteCommands(commands, SetIdleState);
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
    private List<PathfinderNodeBase> CalculatePath(Vector3 targetPosition, int interactDistance = 0)
    {
        List<Vector2> ignoringNodes = new List<Vector2>();
        Vector2Int startNodeCoord = GridManager.Grid.Grid.GetPositionOnGrid(transform.position);
        Vector2Int targetNodeCoord = GridManager.Grid.Grid.GetPositionOnGrid(targetPosition);
        List<PathfinderNodeBase> path = GridManager.FindPath(
            startNodeCoord,
            targetNodeCoord,
            new List<Vector2> { startNodeCoord, targetNodeCoord },
            interactDistance);
        if (path == null) throw new Exception("Path is null");

        return path;
    }

    private void OnExecutionComplete()
    {
        SetIdleState();
        ExecutionComplited?.Invoke();
    }
    #endregion
}
