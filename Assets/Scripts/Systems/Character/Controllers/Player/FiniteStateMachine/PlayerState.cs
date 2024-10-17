using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class PlayerState
{
    protected PlayerIngameController _playerController;
    protected IInputHandler _inputHandler;
    protected PlayerStateMachine _stateMachine;
    protected CharacterInfo _player;

    #region init
    public PlayerState(
        PlayerIngameController playerController,
        IInputHandler inputHandler,
        PlayerStateMachine stateMachine,
        CharacterInfo player)
    {
        _playerController = playerController;
        _inputHandler = inputHandler;
        _stateMachine = stateMachine;
        _player = player;
    }
    #endregion

    #region state controll
    public abstract void EnterState();

    public abstract void ExitState();
    #endregion

    #region external operations
    public virtual void NewTurn()
    {
        _playerController.NewTurn();
    }
    #endregion

    #region internal operations
    protected List<PathfinderNodeBase> CalculatePath(Vector3 targetPosition, int interactDistance = 0)
    {
        List<Vector2> ignoringNodes = new List<Vector2>();
        Vector2Int startNodeCoord = _stateMachine.GridManager.Grid.Grid.GetPositionOnGrid(_playerController.transform.position);
        Vector2Int targetNodeCoord = _stateMachine.GridManager.Grid.Grid.GetPositionOnGrid(targetPosition);
        List<PathfinderNodeBase> path = _stateMachine.GridManager.FindPath(
            startNodeCoord,
            targetNodeCoord,
            new List<Vector2> { startNodeCoord, targetNodeCoord },
            interactDistance);
        if (path == null) throw new Exception("Path is null");

        return path;
    }
    #endregion
}
