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
        PlayerStateMachine stateMachine,
        PlayerIngameController playerController,
        IInputHandler inputHandler,
        CharacterInfo player)
    {
        _stateMachine = stateMachine;
        _playerController = playerController;
        _inputHandler = inputHandler;
        _player = player;
    }
    #endregion

    #region properties
    public PlayerStateMachine StateMachine { get => _stateMachine; }
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

    protected Vector2 GetMousePosition()
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    protected TileNode GetNodeByMousePosition(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                return _stateMachine.GridManager.Grid.Grid.GetNode(mousePosition);
            }
        }

        return null;
    }
    #endregion
}
