using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CharacterIngameController : MonoBehaviour
{
    private CharacterBlank _character;
    protected CharacterInfo _characterInfo;
    protected Animator _animator;

    private IMovable _movable;
    private IAttackable _attackable;
    private ActionList _skills;
    private ActionCommandList _commandList;

    private GridManager _gridManager;

    #region events
    public event Action<bool, string> ExecutionEnded;
    #endregion

    public virtual void Setup()
    {
        _character = GetComponent<CharacterBlank>();
        _characterInfo = GetComponent<CharacterInfo>();
        _animator = GetComponentInChildren<Animator>();

        _movable = GetComponentInChildren<IMovable>();
        _attackable = GetComponentInChildren<Attackable>();
        _skills = GetComponentInChildren<ActionList>();

        _movable.Setup(_characterInfo, _animator);
        _attackable.Setup(_characterInfo, _animator);

        _commandList = new ActionCommandList();
        _commandList.ExecutionEnded += CommnadListExecutionEndedHandler;

    }

    private void OnDestroy()
    {
        _commandList.ExecutionEnded -= CommnadListExecutionEndedHandler;
    }

    public GridManager GridManager 
    {  
        get => _gridManager;
        set => _gridManager = value;
    }

    #region external interactions
    public void NewTurn()
    {
        _character.ApMpSystem.ResetAll();
    }
    
    public void Rotate(Vector3 targetPosition)
        => _commandList.ExecuteCommands(new RotateCommand(_movable, targetPosition));

    public void Walk(List<PathfinderNodeBase> path)
        => _commandList.ExecuteCommands(new MoveCommand(_movable, path));

    public void Attack(IDamagable target)
        => _commandList.ExecuteCommands(new AttackCommand(_attackable, target));

    public void WalkAndAttack(List<PathfinderNodeBase> path, IDamagable target, Vector3 targetPosition, int interactDistance)
    {
        List<ActionCommandBase> commands = new List<ActionCommandBase>();
        commands.Add(new MoveCommand(_movable, path));
        commands.Add(new RotateCommand(_movable, targetPosition));
        commands.Add(new AttackCommand(_attackable, target));
        _commandList.ExecuteCommands(commands);
    }

    public void WalkAndAct(List<PathfinderNodeBase> path, ActionCommandBase action, Vector3 targetPosition)
    {
        List<ActionCommandBase> commands = new List<ActionCommandBase>();
        commands.Add(new MoveCommand(_movable, path));
        commands.Add(new RotateCommand(_movable, targetPosition));
        commands.Add(action);
        _commandList.ExecuteCommands(commands);
    }

    public void WalkAndInteract(Vector3 targetPoisition, ActionCommandBase action, int interactionRadius)
    {

    }
    #endregion

    #region event handlers
    private void CommnadListExecutionEndedHandler(bool status, string message)
    {
        ExecutionEnded?.Invoke(status, message);
    }
    #endregion

    #region internal oprations
    protected List<PathfinderNodeBase> CalculatePath(Vector3 targetPosition, int interactDistance = 0)
    {
        List<Vector2> ignoringNodes = new List<Vector2>();
        Vector2Int startNodeCoord = _gridManager.Grid.Grid.GetPositionOnGrid(transform.position);
        Vector2Int targetNodeCoord = _gridManager.Grid.Grid.GetPositionOnGrid(targetPosition);
        List<PathfinderNodeBase> path = _gridManager.FindPath(
            startNodeCoord,
            targetNodeCoord,
            new List<Vector2> { startNodeCoord, targetNodeCoord },
            interactDistance);
        if (path == null) throw new Exception("Path is null");

        return path;
    }
    #endregion
}
