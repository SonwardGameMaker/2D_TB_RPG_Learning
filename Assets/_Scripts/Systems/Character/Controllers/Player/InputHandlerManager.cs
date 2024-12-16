using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerManager : ControllerManagerBase
{
    private PlayerStateMachine _stateMachine;

    [SerializeField] private UiManager _uiManager;

    [Header("Debug")]
    [SerializeField] private int _attackRadius = 0;

    #region init
    public override void Setup()
    {
        base.Setup();
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
