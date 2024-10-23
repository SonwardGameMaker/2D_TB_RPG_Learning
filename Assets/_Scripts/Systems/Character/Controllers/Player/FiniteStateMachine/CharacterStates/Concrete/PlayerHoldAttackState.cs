using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerHoldAttackState : PlayerState
{
    #region init
    public PlayerHoldAttackState(
        PlayerStateMachine stateMachine,
        PlayerIngameController playerController,
        IInputHandler inputHandler,
        CharacterInfo player) : base(stateMachine, playerController, inputHandler, player) { }
    #endregion

    #region state control
    public override void EnterState()
    {
        _inputHandler.LMB_Pressed += Attack;

        _inputHandler.AttackMode_Released += ChangeToIdleState;

        _stateMachine.UiManager.SetAttackCursor();
    }

    public override void ExitState()
    {
        _inputHandler.LMB_Pressed -= Attack;

        _inputHandler.AttackMode_Released -= ChangeToIdleState;

        _stateMachine.UiManager.SetBaseCursor();
    }
    #endregion

    #region input event handlers
    private void Attack()
    {
        Vector3 mousePosition = GetMousePosition();
        TileNode targetNode = GetNodeByMousePosition(mousePosition);

        if (targetNode == null || !targetNode.IsWalkable) return;

        CharacterInfo characterOnTargetTile = targetNode.CharacterOnTile;

        if (characterOnTargetTile != null && characterOnTargetTile.tag == "Characters"
            && characterOnTargetTile.GetComponentInChildren<IDamagable>() != null)
        {
            _playerController.WalkAndAttack(
                CalculatePath(mousePosition, _stateMachine.AttackRadius),
                characterOnTargetTile.GetComponentInChildren<IDamagable>(),
                mousePosition,
                _stateMachine.AttackRadius);
        }
    }
    private void ChangeToIdleState()
    {
        _stateMachine.ChangeState<PlayerIdleState>();
    }
    #endregion
}
