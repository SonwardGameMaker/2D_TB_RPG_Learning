using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerIngameController _playerController;
    [SerializeField] private IngameGrid _grid;

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
            Walk();
        }
    }

    private void Walk()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //Debug.Log($"Mouse position: {mousePosition.x}, {mousePosition.y}");

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                List<PathfinderNodeBase> path = _grid.FindPath(_playerController.transform.position, mousePosition);
                //Debug.Log($"Path cost: {CalculatePathCost(path)}");
                _playerController.Walk(path);
            }
        }
    }
    private int CalculatePathCost(List<PathfinderNodeBase> path)
        => path.Sum(pn => pn.CameFromCost);

    private void WriteTileInfo()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //Debug.Log($"Mouse position: {mousePosition.x}, {mousePosition.y}");

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Grid")
            {
                IngameGrid grid = hit.collider.GetComponent<IngameGrid>();
                TileNode nodeSelected = hit.collider.GetComponent<IngameGrid>().Grid.GetNode(mousePosition);
                Vector2 nodeCoordinates = nodeSelected.Coordinates;
                Debug.Log($"Node selected: {nodeCoordinates.x}, {nodeCoordinates.y}; Is {(nodeSelected.CanCharacerWalk(_characterInfo) ? "" : "un")}walkable" +
                    $"{(nodeSelected.CharacterOnTile != null ? "\nCharacter on tile: " + nodeSelected.CharacterOnTile.name : "")}");
            }
        }
    }
}
