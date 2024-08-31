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
    public GridSystem(int width, int height, float cellSize, Vector3 originPosition,
        List<(Vector3, Environment)> environment,
        List<(Vector3, CharacterInfo)> characters)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _grid = CreateGrid(width, height);
        InitGridEnvironment(environment);
        InitCharactersOnGrid(characters);
    }

    private TileNode[,] CreateGrid(int width, int height)
    {
        TileNode[,] result = new TileNode[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x,y] = new TileNode(x, y, this);
            }
        }

        return result;
    }
    private void InitGridEnvironment(List<(Vector3, Environment)> environment)
    {
        if (environment == null) Debug.LogError("Environment is null");
        if (environment.Count == 0) Debug.LogWarning("Environment is empty");

        foreach ((Vector3, Environment) env in environment)
        {
            GetNode(env.Item1).TrySetEnvironment(env.Item2);
        }
    }
    private void InitCharactersOnGrid(List<(Vector3, CharacterInfo)> characters)
    {
        if (characters == null) Debug.LogError("Characters is null");
        if (characters.Count == 0) Debug.LogWarning("Characters is empty");

        foreach ((Vector3, CharacterInfo) character in characters)
        {
            GetNode(character.Item1).TrySetCharacter(character.Item2);
        }
    }
    #endregion

    #region properties
    public int Width { get => _width;  }
    public int Height { get => _height; }
    public float CellSize { get => _cellSize; }
    public Vector3 OriginPosition { get => _originPosition; set => _originPosition = value; }
    #endregion

    #region external interactions
    public TileNode GetNode(Vector3 worldPosition)
    {
        Vector2Int positionOnGrid = GetPositionOnGrid(worldPosition);
        return _grid[positionOnGrid.x, positionOnGrid.y];
    }
    public TileNode GetNode(int x, int y)
        => _grid[x, y];
    public TileNode GetNode(Vector2Int positionOnGrid)
        => _grid[positionOnGrid.x, positionOnGrid.y];

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
