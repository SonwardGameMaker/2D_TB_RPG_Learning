using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerManager : ControllerManagerBase
{
    private PlayerIngameController _playerController;
    private IInputHandler _inputHandler;
    private CharacterInfo _player;

    [Header("Debug")]
    [SerializeField] private int _attackRadius = 0;

    #region init
    private void Start()
    {
        _player = GetComponent<CharacterInfo>();

        _playerController = GetComponent<PlayerIngameController>();
        _playerController.ExecutionEnded += ExecutionEndedHandler;

        _inputHandler = GetComponentInChildren<IInputHandler>();
        _inputHandler.LMB_Pressed += Select;
        _inputHandler.RMB_Pressed += Interact;
    }

    private void OnDestroy()
    {
        _playerController.ExecutionEnded -= ExecutionEndedHandler;

        _inputHandler.LMB_Pressed -= Select;
        _inputHandler.RMB_Pressed -= Interact;
    }
    #endregion

    #region external interactions
    public override void NewTurn()
    {
        _playerController.NewTurn();
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
    }

    private void Interact()
    {
        if (GridManager == null)
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
                CalculatePath(mousePosition, _attackRadius),
                characterOnTargetTile.GetComponentInChildren<IDamagable>(),
                mousePosition,
                _attackRadius);
        }
        else
        {
            _playerController.Walk(CalculatePath(mousePosition));
        }
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
                return GridManager.Grid.Grid.GetNode(mousePosition);
            }
        }

        return null;
    }
    #endregion
}
