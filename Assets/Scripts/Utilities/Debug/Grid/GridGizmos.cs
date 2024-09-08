using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GridGizmos
{
    public Color GridColor = Color.white;
    public Color PathColor = Color.green;
    public Color TargetingColor = Color.magenta;
    public Color TargetingTilesColor = Color.red;

    public GameObject TargetPathTilePrefab;

    private GridSystem _targetGrid;

    public GridGizmos(GridSystem targetGrid)
    {
        _targetGrid = targetGrid; 
    }

    #region external interactions
    public Vector3 GetWorldPosition(int x, int y)
        => new Vector3(x, y) * _targetGrid.CellSize + _targetGrid.OriginPosition;
    public Vector3 GetWorldPosition(TileNode tile)
    => GetWorldPosition(tile.X, tile.Y);
    public Vector3 GetWorldPosition(PathfinderNodeBase tile)
        => GetWorldPosition(tile.X, tile.Y);

    public Vector2Int GetPositionOnGrid(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _targetGrid.OriginPosition).x / _targetGrid.CellSize);
        int y = Mathf.FloorToInt((worldPosition - _targetGrid.OriginPosition).y / _targetGrid.CellSize);
        return new Vector2Int(x, y);
    }
    #endregion

    #region debug
    public void DrawGridGizmos()
    {
        for (int x = 0; x < _targetGrid.Width; x++)
        {
            for (int y = 0; y < _targetGrid.Height; y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), GridColor, Int32.MaxValue);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), GridColor, Int32.MaxValue);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, _targetGrid.Height), GetWorldPosition(_targetGrid.Width, _targetGrid.Height), GridColor, Int32.MaxValue);
        Debug.DrawLine(GetWorldPosition(_targetGrid.Width, 0), GetWorldPosition(_targetGrid.Width, _targetGrid.Height), GridColor, Int32.MaxValue);
    }
    public void DrawPathGizmos(List<PathfinderNodeBase> pathfinderNodes)
    {
        if (pathfinderNodes == null) throw new ArgumentNullException("Path list is null");

        foreach (var node in pathfinderNodes)
        {
            if (node.CameFromNode != null)
            {
                Debug.DrawLine(GetWorldPosition(node.CameFromNode.X, node.CameFromNode.Y) + new Vector3(_targetGrid.CellSize / 2, _targetGrid.CellSize / 2, 0),
                GetWorldPosition(node.X, node.Y) + new Vector3(_targetGrid.CellSize / 2, _targetGrid.CellSize / 2, 0),
                PathColor,
                100f);
            }
        }
    }

    public void DrawDistaneGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {
        //Debug.Log("Drawing distance line");
        Vector3 startPosition = GetWorldPosition(currentNode.X, currentNode.Y) + new Vector3(_targetGrid.CellSize / 2, _targetGrid.CellSize / 2, 0);
        Vector3 endPosition = GetWorldPosition(targetNode.X, targetNode.Y) + new Vector3(_targetGrid.CellSize / 2, _targetGrid.CellSize / 2, 0);
        //Debug.Log($"Start: {startPosition.x}, {startPosition.y}; End: {endPosition.x}, {endPosition.y}");
        Debug.DrawLine(startPosition, endPosition, TargetingColor, 100f);
    }

    public void DrawTargetingPathGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {
        SpawnTargetPathTiles(GetLineCells(new Vector2Int(currentNode.X, currentNode.Y),
            new Vector2Int(targetNode.X, targetNode.Y)));
    }

    public void SpawnTargetPathTiles(List<TileNode> targetingPath)
    {
        foreach (TileNode tile in targetingPath)
        {
            Vector3 spawnPoint = GetWorldPosition(tile) + new Vector3(_targetGrid.CellSize / 2, _targetGrid.CellSize / 2, 0);

            GameObject targetingPrefab = UnityEngine.Object.Instantiate(TargetPathTilePrefab);

            targetingPrefab.transform.position = spawnPoint;
            targetingPrefab.GetComponent<SpriteRenderer>().color = TargetingTilesColor;

            UnityEngine.Object.Destroy(targetingPrefab, 100f);
        }
    }

    public List<TileNode> GetLineCells(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> result = General.GetLineCells(start, end);

        return result.Select(cr => _targetGrid.GetNode(cr.x, cr.y)).ToList();
    }

    #endregion
}
