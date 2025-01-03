using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerIdleState : PlayerState
{
    #region init
    public PlayerIdleState(
        PlayerStateMachine stateMachine,
        PlayerIngameController playerController,
        IInputHandler inputHandler,
        CharacterInfo player) : base(stateMachine, playerController, inputHandler, player) { }
    #endregion

    #region state controll
    public override void EnterState()
    {
        _playerController.ExecutionEnded += ExecutionEndedHandler;

        _inputHandler.LMB_Pressed += Select;
        _inputHandler.RMB_Pressed += Interact;

        _inputHandler.ChangeWeapon_Pressed += ChangeWeapon;

        _inputHandler.FirstCellButton_Pressed += ActivateFirstCell;
        _inputHandler.SecondCellButton_Pressed += ActivateSecondCell;

        _inputHandler.AttackMode_Pressed += ChangeToHoldAttackMode;
    }

    public override void ExitState()
    {
        _playerController.ExecutionEnded -= ExecutionEndedHandler;

        _inputHandler.LMB_Pressed -= Select;
        _inputHandler.RMB_Pressed -= Interact;

        _inputHandler.ChangeWeapon_Pressed -= ChangeWeapon;

        _inputHandler.FirstCellButton_Pressed -= ActivateFirstCell;
        _inputHandler.SecondCellButton_Pressed -= ActivateSecondCell;

        _inputHandler.AttackMode_Pressed -= ChangeToHoldAttackMode;
    }
    #endregion

    #region input event handlers
    private void Select()
    {
        TileNode nodeSelected = GetNodeByMousePosition(GetMousePosition());
        if (nodeSelected == null)
        {
            Debug.Log("Selected node is null");
            return;
        }

        Vector2 nodeCoordinates = nodeSelected.Coordinates;
        Debug.Log($"Node selected: {nodeCoordinates.x}, {nodeCoordinates.y}; Is {(nodeSelected.CanCharacerWalk(_player) ? "" : "un")}walkable" +
            $"{(nodeSelected.CharacterOnTile != null ? "\nCharacter on tile: " + nodeSelected.CharacterOnTile.name : "")}");
        if (nodeSelected.CharacterOnTile != null)
            Debug.Log(nodeSelected.CharacterOnTile.GetComponent<CharacterInfo>().GetBaseInfoString());
    }

    private void Interact()
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
        else
        {
            _playerController.Walk(CalculatePath(mousePosition));
        }
    }

    private void ChangeWeapon()
    {
        _player.CharacterCombatInfo.ChangeWeapon();
    }

    private void ChangeToHoldAttackMode()
    {
        _stateMachine.ChangeState<PlayerHoldAttackState>();
    }

    private void ActivateFirstCell()
    {
        _stateMachine.FirstCell.Activate();
    }
        
    private void ActivateSecondCell()
    {
        _stateMachine.SecondCell.Activate();
    }
    #endregion

    #region event handlers
    private void ExecutionEndedHandler(bool status, string message)
    {
        if (!status)
        {
            Debug.Log(message);
        }
    }
    #endregion
}
