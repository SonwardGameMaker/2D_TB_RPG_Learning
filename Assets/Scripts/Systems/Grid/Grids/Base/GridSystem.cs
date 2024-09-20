using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem<TGridObjet> where TGridObjet : NodeBase
{
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private TGridObjet[,] _grid;

    #region init
    public GridSystem(int width, int height, float cellSize, Vector3 originPosition, Func<int, int, TGridObjet> NodeInitFunc)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _grid = CreateGrid(width, height, NodeInitFunc);
    }

    private TGridObjet[,] CreateGrid(int width, int height, Func<int, int, TGridObjet> NodeInitFunc)
    {
        TGridObjet[,] result = new TGridObjet[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x, y] = NodeInitFunc(x, y);
            }
        }

        return result;
    }
    #endregion

    #region properties
    public int Width { get => _width; }
    public int Height { get => _height; }
    public float CellSize { get => _cellSize; }
    public Vector3 OriginPosition { get => _originPosition; set => _originPosition = value; }
    #endregion

    #region external interactions
    public TGridObjet GetNode(Vector3 worldPosition)
    {
        Vector2Int positionOnGrid = GetPositionOnGrid(worldPosition);
        return _grid[positionOnGrid.x, positionOnGrid.y];
    }

    public TGridObjet GetNode(int x, int y)
        => _grid[x, y];

    public TGridObjet GetNode(Vector2Int positionOnGrid)
        => _grid[positionOnGrid.x, positionOnGrid.y];

    public Vector3 GetWorldPosition(int x, int y)
            => new Vector3(x, y) * _cellSize + _originPosition;

    public Vector3 GetWorlPositionOfTileCenter(int x, int y)
        => new Vector3(x, y) * _cellSize + _originPosition + new Vector3(_cellSize / 2.0f, _cellSize / 2.0f);

    public Vector2Int GetPositionOnGrid(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        int y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
        return new Vector2Int(x, y);
    }
    #endregion
}
