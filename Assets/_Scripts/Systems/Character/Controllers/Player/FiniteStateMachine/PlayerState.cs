using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
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
        => _playerController.CalculatePath(targetPosition, interactDistance);

    protected Vector2 GetMousePosition()
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    protected TileNode GetNodeByMousePosition(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                return _playerController.GridManager.Grid.Grid.GetNode(mousePosition);
            }
        }

        return null;
    }
    #endregion
}
