using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ControllerManagerBase : MonoBehaviour
{
    public virtual GridManager GridManager { get; set; }
    public GameManager GameManager { get; set; }

    public abstract void NewTurn();
    public void EndTurn()
    {
        
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
}
