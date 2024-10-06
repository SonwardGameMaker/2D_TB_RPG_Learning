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

    private void Start()
    {
        Debug.Log("Player manager start");

        _playerController = GetComponent<PlayerIngameController>();
        _inputHandler = GetComponentInChildren<IInputHandler>();
        _inputHandler.LMB_Pressed += Select;
        _inputHandler.RMB_Pressed += Interact;
    }

    private void OnDestroy()
    {
        _inputHandler.LMB_Pressed -= Select;
        _inputHandler.RMB_Pressed -= Interact;
    }

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
        if (_gridManager == null)
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
            _playerController.WalkAndAttack(characterOnTargetTile.GetComponentInChildren<IDamagable>(),
                mousePosition,
                _attackRadius);
        }
        else
        {
            _playerController.Walk(GetMousePosition());
        }
    }

    private Vector2 GetMousePosition()
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private TileNode GetNodeByMousePosition(Vector2 mousePosition)
    {
        Debug.Log("Funck started");
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        Debug.Log("Hitted");

        if (hit.collider != null)
        {
            Debug.Log("Hitted collider");
            if (_playerController.ControllerState == ControllerStates.Idle)
            {
                if (hit.collider.gameObject.tag == "Grid")
                {
                    Debug.Log("trying to get node");
                    return _gridManager.Grid.Grid.GetNode(mousePosition);
                }
                else
                {
                    Debug.Log("Didn't hit the Grid. Hitted the " + hit.collider.gameObject.tag);
                }
            }
            else
            {
                Debug.Log("Controller is busy");
            }
        }
        else
        {
            Debug.Log("Hit is null");
        }

        return null;
    }
}
