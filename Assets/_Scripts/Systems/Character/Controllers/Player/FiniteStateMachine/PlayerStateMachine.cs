using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerStateMachine
{
    private PlayerState _currentState;
    private GridManager _gridManager;
    private UiManager _uiManager;

    private List<PlayerState> _stateList;

    #region init
    public PlayerStateMachine(UiManager uiManager, InputHandlerManager inputHandler)
    {
        _uiManager = uiManager;

        PlayerIngameController controller = inputHandler.GetComponent<PlayerIngameController>();
        IInputHandler input = inputHandler.GetComponentInChildren<IInputHandler>();
        CharacterInfo player = inputHandler.GetComponent<CharacterInfo>();

        _stateList = new List<PlayerState>();
        _stateList.Add(new PlayerIdleState(this, controller, input, player));
        _stateList.Add(new PlayerHoldAttackState(this, controller, input, player));
        _stateList.Add(new PlayerAttackState(this, controller, input, player));
    }
    public void Setup<T>() where T : PlayerState
    {
        _currentState = _stateList.Find(s => s is T) as T;
        _currentState.EnterState();
    }

    ~PlayerStateMachine()
    {
        _currentState.ExitState();
    }
    #endregion

    #region properties
    public PlayerState CurrentState { get => _currentState; }
    public GridManager GridManager 
    { 
        get => _gridManager;
        set => _gridManager = value;
    }
    public UiManager UiManager { get => _uiManager; }
    public int AttackRadius { get; set; }
    #endregion

    #region external interactions
    public void ChangeState<T>() where T : PlayerState
    {
        _currentState.ExitState();
        _currentState = _stateList.Find(s => s as T is T) as T;
        _currentState.EnterState();
    }
    #endregion
}
