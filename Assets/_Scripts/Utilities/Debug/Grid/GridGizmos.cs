using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GridGizmos : MonoBehaviour
{
    public LogicalGrid TargetGrid;
    public Color GridColor = Color.white;
    public Color PathColor = Color.green;
    public Color TargetingColor = Color.magenta;
    public Color TargetingTilesColor = Color.red;
    [SerializeField] private bool GridDebug;
    [SerializeField] private bool PathfinderDebug;
    [SerializeField] private bool DrawAffectedTiles;

    public GameObject TargetPathTilePrefab;

    #region external interactions
    public Vector3 GetWorldPosition(int x, int y)
        => new Vector3(x, y) * TargetGrid.Grid.CellSize + TargetGrid.Grid.OriginPosition;
    public Vector3 GetWorldPosition(TileNode tile)
    => GetWorldPosition(tile.X, tile.Y);
    public Vector3 GetWorldPosition(PathfinderNodeBase tile)
        => GetWorldPosition(tile.X, tile.Y);

    public Vector2Int GetPositionOnGrid(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - TargetGrid.Grid.OriginPosition).x / TargetGrid.Grid.CellSize);
        int y = Mathf.FloorToInt((worldPosition - TargetGrid.Grid.OriginPosition).y / TargetGrid.Grid.CellSize);
        return new Vector2Int(x, y);
    }
    #endregion

    #region debug
    public void DrawGridGizmos()
    {
        if (!GridDebug) return;

        for (int x = 0; x < TargetGrid.Grid.Width; x++)
        {
            for (int y = 0; y < TargetGrid.Grid.Height; y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), GridColor, Int32.MaxValue);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), GridColor, Int32.MaxValue);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, TargetGrid.Grid.Height), GetWorldPosition(TargetGrid.Grid.Width, TargetGrid.Grid.Height), GridColor, Int32.MaxValue);
        Debug.DrawLine(GetWorldPosition(TargetGrid.Grid.Width, 0), GetWorldPosition(TargetGrid.Grid.Width, TargetGrid.Grid.Height), GridColor, Int32.MaxValue);
    }
    public void DrawPathGizmos(List<PathfinderNodeBase> pathfinderNodes)
    {
        if (!PathfinderDebug) return;

        if (pathfinderNodes == null) throw new ArgumentNullException("Path list is null");

        foreach (var node in pathfinderNodes)
        {
            if (node.CameFromNode != null)
            {
                Debug.DrawLine(GetWorldPosition(node.CameFromNode.X, node.CameFromNode.Y) + new Vector3(TargetGrid.Grid.CellSize / 2, TargetGrid.Grid.CellSize / 2, 0),
                GetWorldPosition(node.X, node.Y) + new Vector3(TargetGrid.Grid.CellSize / 2, TargetGrid.Grid.CellSize / 2, 0),
                PathColor,
                100f);
            }
        }
    }

    public void DrawDistaneGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {
        if (!PathfinderDebug) return;

        //Debug.Log("Drawing distance line");
        Vector3 startPosition = GetWorldPosition(currentNode.X, currentNode.Y) + new Vector3(TargetGrid.Grid.CellSize / 2, TargetGrid.Grid.CellSize / 2, 0);
        Vector3 endPosition = GetWorldPosition(targetNode.X, targetNode.Y) + new Vector3(TargetGrid.Grid.CellSize / 2, TargetGrid.Grid.CellSize / 2, 0);
        //Debug.Log($"Start: {startPosition.x}, {startPosition.y}; End: {endPosition.x}, {endPosition.y}");
        Debug.DrawLine(startPosition, endPosition, TargetingColor, 100f);
    }

    public void DrawTargetingPathGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {
        if (!DrawAffectedTiles) return;
        SpawnTargetPathTiles(GetLineCells(new Vector2Int(currentNode.X, currentNode.Y),
            new Vector2Int(targetNode.X, targetNode.Y)));
    }

    private void SpawnTargetPathTiles(List<TileNode> targetingPath)
    {
        foreach (TileNode tile in targetingPath)
        {
            Vector3 spawnPoint = GetWorldPosition(tile) + new Vector3(TargetGrid.Grid.CellSize / 2, TargetGrid.Grid.CellSize / 2, 0);

            GameObject targetingPrefab = UnityEngine.Object.Instantiate(TargetPathTilePrefab);

            targetingPrefab.transform.position = spawnPoint;
            targetingPrefab.GetComponent<SpriteRenderer>().color = TargetingTilesColor;

            UnityEngine.Object.Destroy(targetingPrefab, 100f);
        }
    }

    public List<TileNode> GetLineCells(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> result = General.GetLineCells(start, end);

        return result.Select(cr => TargetGrid.Grid.GetNode(cr.x, cr.y)).ToList();
    }

    #endregion
}
