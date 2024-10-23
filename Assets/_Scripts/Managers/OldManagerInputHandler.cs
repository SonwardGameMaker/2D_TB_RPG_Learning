using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OldManagerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerIngameController _playerController;
    [SerializeField] private GridManager _grid;

    // Debug 
    [SerializeField] private int _attackRadius;

    private CharacterInfo _characterInfo;

    private void Start()
    {
        _characterInfo = _playerController.GetComponent<CharacterInfo>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WriteTileInfo();
        }
        if (Input.GetMouseButtonDown(1))
        {
            WalkAndShoot();
        }
    }

    private void WalkAndShoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                TileNode targetNode = _grid.Grid.Grid.GetNode(mousePosition);
                if (!targetNode.IsWalkable) return;
                CharacterInfo characterOnTargetTile = targetNode.CharacterOnTile;

                if (characterOnTargetTile != null && characterOnTargetTile.tag == "Characters"
                    && characterOnTargetTile.GetComponentInChildren<IDamagable>() != null)
                {
                    //_playerController.WalkAndAttack(characterOnTargetTile.GetComponentInChildren<IDamagable>(),
                    //    mousePosition,
                    //    _attackRadius);
                }
                else
                {
                    //_playerController.Walk(mousePosition);
                }
            }
            else
            {
                Debug.Log("Character still busy");
            }
        }
    }
    private int CalculatePathCost(List<PathfinderNodeBase> path)
        => path.Sum(pn => pn.CameFromCost);

    private void WriteTileInfo()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                GridManager grid = hit.collider.GetComponent<GridManager>();
                TileNode nodeSelected = hit.collider.GetComponent<LogicalGrid>().Grid.GetNode(mousePosition);
                Vector2 nodeCoordinates = nodeSelected.Coordinates;
                Debug.Log($"Node selected: {nodeCoordinates.x}, {nodeCoordinates.y}; Is {(nodeSelected.CanCharacerWalk(_characterInfo) ? "" : "un")}walkable" +
                    $"{(nodeSelected.CharacterOnTile != null ? "\nCharacter on tile: " + nodeSelected.CharacterOnTile.name : "")}");
                if (nodeSelected.CharacterOnTile != null)
                    Debug.Log(nodeSelected.CharacterOnTile.GetComponent<CharacterInfo>().GetBaseInfoString());
            }
        }
    }
}
