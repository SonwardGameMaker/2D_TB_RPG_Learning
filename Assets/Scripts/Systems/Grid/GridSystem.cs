using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private TileNode[,] _grid;

    #region init
    public GridSystem(int width, int height, float cellSize, Vector3 originPosition)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _grid = CreateGrid(width, height);

        DrawGridGizmos();
    }

    private TileNode[,] CreateGrid(int width, int height)
    {
        TileNode[,] result = new TileNode[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x,y] = new TileNode(x, y);
            }
        }

        return result;
    }
    #endregion

    #region properties
    public int Width { get => _width;  }
    public int Height { get => _height; }
    public float CellSize { get => _cellSize; }
    #endregion

    #region external interactions
    public TileNode GetNode(Vector3 worldPosition)
    {
        Vector2Int positionOnGrid = GetPositionOnGrid(worldPosition);
        return _grid[positionOnGrid.x, positionOnGrid.y];
    }
    public TileNode GetNode(int x, int y)
        => _grid[x, y];

    public bool MoveChararacter(Vector3 characterPosition, Vector3 targetPosition)
        => MoveChararacter(GetPositionOnGrid(characterPosition), GetPositionOnGrid(targetPosition));
    public bool MoveChararacter(Vector2Int characterPosition, Vector2Int targetPosition)
    {
        TileNode characterTile = _grid[characterPosition.x, characterPosition.y];

        if (characterTile.CharacterOnTile == null)
            throw new Exception($"No characters on tile {characterTile.X}, {characterTile.Y}");
        //return false;

        if (_grid[targetPosition.x, targetPosition.y].TrySetCharacter(characterTile.CharacterOnTile))
        {
            characterTile.TryRemoveCharacter();
            return true;
        }
        return false;
    }
    #endregion

    #region internal calculations
    private Vector2Int GetPositionOnGrid(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        int y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
        return new Vector2Int(x, y);
    }

    private Vector3 GetWorlPosition(int x, int y)
        => new Vector3(x, y) * _cellSize + _originPosition;
    #endregion

    #region debug
    private void DrawGridGizmos()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorlPosition(0, _height), GetWorlPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorlPosition(_width, 0), GetWorlPosition(_width, _height), Color.white, 100f);
    }
    #endregion
}
