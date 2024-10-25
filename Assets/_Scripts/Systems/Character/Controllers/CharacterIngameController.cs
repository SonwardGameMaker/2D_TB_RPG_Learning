using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterIngameController : MonoBehaviour
{
    private CharacterBlank _character;
    protected CharacterInfo _characterInfo;
    protected Animator _animator;

    private IMovable _movable;
    private IAttackable _attackable;

    private ActionCommandList _commandList;


    #region events
    public event Action<bool, string> ExecutionEnded;
    #endregion

    protected virtual void Start()
    {
        _character = GetComponent<CharacterBlank>();
        _characterInfo = GetComponent<CharacterInfo>();
        _animator = GetComponentInChildren<Animator>();

        _movable = GetComponentInChildren<IMovable>();
        _attackable = GetComponentInChildren<IAttackable>();

        _movable.Setup(_characterInfo, _animator);
        _attackable.Setup(_characterInfo, _animator);

        _commandList = new ActionCommandList();
        _commandList.ExecutionEnded += CommnadListExecutionEndedHandler;

    }

    private void OnDestroy()
    {
        _commandList.ExecutionEnded -= CommnadListExecutionEndedHandler;
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

    internal void WalkAndAct(List<PathfinderNodeBase> path, ActionCommandBase action, Vector3 targetPosition, int interactDistance)
    {
        List<ActionCommandBase> commands = new List<ActionCommandBase>();
        commands.Add(new MoveCommand(_movable, path));
        commands.Add(new RotateCommand(_movable, targetPosition));
        commands.Add(action);
        _commandList.ExecuteCommands(commands);
    }
    #endregion

    #region event handlers
    private void CommnadListExecutionEndedHandler(bool status, string message)
    {
        ExecutionEnded?.Invoke(status, message);
    }
      
    #endregion

    #region internal oprations
    #endregion
}
