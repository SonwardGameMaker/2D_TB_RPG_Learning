using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerIngameController _playerController;
    [SerializeField] private IngameGrid _grid;

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
                List<PathfinderNodeBase> path;
                CharacterInfo characterOnTargetTile = _grid.Grid.GetNode(mousePosition).CharacterOnTile;
                if (characterOnTargetTile != null && characterOnTargetTile.tag == "Characters"
                    && characterOnTargetTile.GetComponentInChildren<IDamagable>() != null)
                {
                    //path = _grid.FindPath(_playerController.transform.position, mousePosition, _characterInfo.CharacterCombatStats.WeaponRange);
                    path = _grid.FindPath(_playerController.transform.position, mousePosition, _attackRadius);
                    _playerController.WalkAndAttack(path, characterOnTargetTile.GetComponentInChildren<IDamagable>());
                }
                else
                {
                    path = _grid.FindPath(_playerController.transform.position, mousePosition);
                    _playerController.Walk(path);
                }
                
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
                IngameGrid grid = hit.collider.GetComponent<IngameGrid>();
                TileNode nodeSelected = hit.collider.GetComponent<IngameGrid>().Grid.GetNode(mousePosition);
                Vector2 nodeCoordinates = nodeSelected.Coordinates;
                Debug.Log($"Node selected: {nodeCoordinates.x}, {nodeCoordinates.y}; Is {(nodeSelected.CanCharacerWalk(_characterInfo) ? "" : "un")}walkable" +
                    $"{(nodeSelected.CharacterOnTile != null ? "\nCharacter on tile: " + nodeSelected.CharacterOnTile.name : "")}");
                if (nodeSelected.CharacterOnTile != null)
                    Debug.Log(nodeSelected.CharacterOnTile.GetComponent<CharacterInfo>().GetBaseInfoString());
            }
        }
    }
}
