using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerIdleState : PlayerState
{
    public PlayerIdleState(
        PlayerIngameController playerController,
        IInputHandler inputHandler,
        PlayerStateMachine stateMachine,
        CharacterInfo player) : base(playerController, inputHandler, stateMachine, player) { }

    #region state controll
    public override void EnterState()
    {
        _playerController.ExecutionEnded += ExecutionEndedHandler;

        _inputHandler.LMB_Pressed += Select;
        _inputHandler.RMB_Pressed += Interact;
    }

    public override void ExitState()
    {
        _playerController.ExecutionEnded -= ExecutionEndedHandler;

        _inputHandler.LMB_Pressed -= Select;
        _inputHandler.RMB_Pressed -= Interact;
    }
    #endregion

    #region input event handlers
    private void Select()
    {
        TileNode nodeSelected = GetNodeByMousePosition(GetMousePosition());

        Vector2 nodeCoordinates = nodeSelected.Coordinates;
        Debug.Log($"Node selected: {nodeCoordinates.x}, {nodeCoordinates.y}; Is {(nodeSelected.CanCharacerWalk(_player) ? "" : "un")}walkable" +
            $"{(nodeSelected.CharacterOnTile != null ? "\nCharacter on tile: " + nodeSelected.CharacterOnTile.name : "")}");
        if (nodeSelected.CharacterOnTile != null)
            Debug.Log(nodeSelected.CharacterOnTile.GetComponent<CharacterInfo>().GetBaseInfoString());
        //Debug.Log("Select()");
    }

    private void Interact()
    {
        if (_stateMachine.GridManager == null)
        {
            Debug.Log("Grid not set");
            return;
        }

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

        //Debug.Log("Interact()");
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

    #region internal operations
    private Vector2 GetMousePosition()
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private TileNode GetNodeByMousePosition(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                return _stateMachine.GridManager.Grid.Grid.GetNode(mousePosition);
            }
        }

        return null;
    }
    #endregion
}
