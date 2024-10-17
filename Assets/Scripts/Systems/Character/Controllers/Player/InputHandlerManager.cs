using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerManager : ControllerManagerBase
{
    private PlayerIngameController _playerController;
    private IInputHandler _inputHandler;
    private CharacterInfo _player;

    private PlayerStateMachine _stateMachine;
    private PlayerIdleState _baseState;

    [Header("Debug")]
    [SerializeField] private int _attackRadius = 0;

    #region init
    private void Start()
    {
        _player = GetComponent<CharacterInfo>();
        _playerController = GetComponent<PlayerIngameController>();
        _inputHandler = GetComponentInChildren<IInputHandler>();

        _baseState = new PlayerIdleState(_playerController, _inputHandler, _stateMachine, _player);
        _stateMachine = new PlayerStateMachine(_baseState, _attackRadius);
    }

    private void OnDestroy()
    {
        _stateMachine = null;
    }
    #endregion

    #region properties
    public override GridManager GridManager 
    { 
        get => base.GridManager;
        set
        {
            _stateMachine.GridManager = value;
            base.GridManager = value;
        } 
    }
    #endregion

    #region Unity triggers
    private void OnValidate()
    {
        if (_stateMachine != null)
        {
            _stateMachine.AttackRadius = _attackRadius;
        }
    }
    #endregion

    #region external interactions
    public override void NewTurn()
    {
        _stateMachine.CurrentState.NewTurn();
    }
    #endregion
}
