using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : ControllerManagerBase
{
    private PlayerStateMachine _stateMachine;

    [SerializeField] private UiManager _uiManager;

    [Header("Debug")]
    [SerializeField] private int _attackRadius = 0;

    #region init
    public override void Setup(CharacterInfo character, CharacterIngameController controller)
    {
        if (!(controller is PlayerIngameController)) throw new Exception("This controller is not for Player");

        _stateMachine = new PlayerStateMachine(_uiManager, this, controller as PlayerIngameController, character);
        _stateMachine.Setup<PlayerIdleState>();
        _stateMachine.AttackRadius = _attackRadius;
    }

    private void OnDestroy()
    {
        _stateMachine = null;
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
