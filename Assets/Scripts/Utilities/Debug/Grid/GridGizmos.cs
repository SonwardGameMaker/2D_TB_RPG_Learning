using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGizmos
{
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
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, _targetGrid.Height), GetWorldPosition(_targetGrid.Width, _targetGrid.Height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_targetGrid.Width, 0), GetWorldPosition(_targetGrid.Width, _targetGrid.Height), Color.white, 100f);
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
                Color.green);
            }
        }
    }
    #endregion
}
