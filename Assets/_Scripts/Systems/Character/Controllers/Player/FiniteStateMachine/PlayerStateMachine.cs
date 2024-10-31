using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerStateMachine
{
    private PlayerState _currentState;
    private GridManager _gridManager;
    private UiManager _uiManager;

    private List<PlayerState> _stateList;
    private List<PlayerBehaviourScriptContainer> _actions;

    #region fixed actions
    private PlayerBehaviourScriptContainer _firstCellContainer;
    private PlayerBehaviourScriptContainer _secondCellContainer;
    private PlayerBehaviourScriptContainer _thirdCellContainer;
    // ...
    #endregion

    #region init
    public PlayerStateMachine(UiManager uiManager, InputHandlerManager inputHandler)
    {
        Debug.Log($"{typeof(PlayerStateMachine)}");
        _uiManager = uiManager;

        PlayerIngameController controller = inputHandler.GetComponent<PlayerIngameController>();
        IInputHandler input = inputHandler.GetComponentInChildren<IInputHandler>();
        CharacterInfo player = inputHandler.GetComponent<CharacterInfo>();

        _stateList = new List<PlayerState>();
        _stateList.Add(new PlayerIdleState(this, controller, input, player));
        _stateList.Add(new PlayerHoldAttackState(this, controller, input, player));
        _stateList.Add(new PlayerAttackState(this, controller, input, player));

        _actions = inputHandler.GetComponentInChildren<ActionList>().AllActionsContainer();
        foreach (var container in _actions)
            container.SetStateMachine(this);

        // Debug
        _firstCellContainer = _actions.Find(a => a is AttackableContainer) as AttackableContainer;
        _secondCellContainer = _actions.Find(a => a is BiteAttackContainer) as BiteAttackContainer;
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
    public PlayerBehaviourScriptContainer FirstCell { get => _firstCellContainer; }
    public PlayerBehaviourScriptContainer SecondCell { get => _secondCellContainer; }
    public PlayerBehaviourScriptContainer ThirdCell { get => _thirdCellContainer; }

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

    public T GetState<T>() where T : PlayerState
        => _stateList.Find(s => s is T) as T;
    #endregion
}
