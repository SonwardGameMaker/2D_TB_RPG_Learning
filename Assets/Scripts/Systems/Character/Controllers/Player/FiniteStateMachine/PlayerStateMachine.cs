using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerStateMachine
{
    private PlayerState _currentState;

    #region init
    public PlayerStateMachine(PlayerState startingState, int attackRadius)
    {
        _currentState = startingState;
        _currentState.EnterState();

        AttackRadius = attackRadius;
        Debug.Log("Player state machine");
    }
    public PlayerStateMachine(PlayerState startingState) : this(startingState, 0) { }

    ~PlayerStateMachine()
    {
        _currentState.ExitState();
    }
    #endregion

    #region properties
    public PlayerState CurrentState { get => _currentState; }
    public GridManager GridManager { get; set; }
    public int AttackRadius { get; set; }
    #endregion

    #region external interactions
    public void ChangeState(PlayerState newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
    #endregion
}
