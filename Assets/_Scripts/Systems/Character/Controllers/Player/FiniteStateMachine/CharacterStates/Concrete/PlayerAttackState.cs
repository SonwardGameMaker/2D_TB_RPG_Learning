using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerAttackState : PlayerState
{
    #region init
    public PlayerAttackState(
        PlayerStateMachine stateMachine,
        PlayerIngameController playerController,
        IInputHandler inputHandler,
        CharacterInfo player) : base(stateMachine, playerController, inputHandler, player) { }
    #endregion

    public IAttackable Attackable { get; set; }

    public override void EnterState()
    {
        _inputHandler.RMB_Pressed += ChangeToIdleState;
        _inputHandler.LMB_Pressed += Attack;

        _inputHandler.CancelButton_Pressed += ChangeToIdleState;

        _stateMachine.UiManager.SetAttackCursor();
    }

    public override void ExitState()
    {
        _inputHandler.RMB_Pressed -= ChangeToIdleState;
        _inputHandler.LMB_Pressed -= Attack;

        _inputHandler.CancelButton_Pressed -= ChangeToIdleState;

        _stateMachine.UiManager.SetBaseCursor();
    }

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
