using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerManager : ControllerManagerBase
{
    [SerializeField] private UiManager _uiManager;

    #region state machine variables
    private PlayerStateMachine _stateMachine;
    #endregion

    [Header("Debug")]
    [SerializeField] private int _attackRadius = 0;

    #region init
    private void Start()
    {
        _stateMachine = new PlayerStateMachine(_uiManager, this); 
        _stateMachine.Setup<PlayerIdleState>();
        _stateMachine.AttackRadius = _attackRadius;
    }

    private void OnDestroy()
    {
        _stateMachine = null;
    }
    #endregion

    #region properties
    public override GridManager GridManager 
    {
        get => _stateMachine.GridManager;
        set => _stateMachine.GridManager = value;
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
