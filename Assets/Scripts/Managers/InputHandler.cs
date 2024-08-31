using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerIngameController _playerController;
    [SerializeField] private IngameGrid _grid;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
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
                Debug.Log($"Path cost: {CalculatePathCost(path)}");
                _playerController.Walk(path);
            }
        }
    }
    private int CalculatePathCost(List<PathfinderNodeBase> path)
        => path.Sum(pn => pn.CameFromCost);
}
