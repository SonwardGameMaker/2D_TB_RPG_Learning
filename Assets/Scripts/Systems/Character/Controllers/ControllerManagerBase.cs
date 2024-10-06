using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManagerBase : MonoBehaviour
{
    protected GridManager _gridManager;

    public GridManager GridManager 
    {
        get { return _gridManager; }
        set
        {
            if (value == null) { Debug.Log("GridManager is null"); return; }
            _gridManager = value;
            Debug.Log($"Grid manager is set on {GetComponent<CharacterInfo>().name}");
            if ( _gridManager == null ) { Debug.Log("GridManager is still null"); }
        } 
    }

    protected List<PathfinderNodeBase> CalculatePath(Vector3 targetPosition, int interactDistance = 0)
    {
        List<Vector2> ignoringNodes = new List<Vector2>();
        Vector2Int startNodeCoord = GridManager.Grid.Grid.GetPositionOnGrid(transform.position);
        Vector2Int targetNodeCoord = GridManager.Grid.Grid.GetPositionOnGrid(targetPosition);
        List<PathfinderNodeBase> path = GridManager.FindPath(
            startNodeCoord,
            targetNodeCoord,
            new List<Vector2> { startNodeCoord, targetNodeCoord },
            interactDistance);
        if (path == null) throw new Exception("Path is null");

        return path;
    }
    protected int CalculatePathCost(List<PathfinderNodeBase> path)
        => path.Sum(pn => pn.CameFromCost);
}
