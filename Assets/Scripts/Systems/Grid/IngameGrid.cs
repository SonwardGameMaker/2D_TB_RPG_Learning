using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IngameGrid : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private EnvironmentContainer _environmentContainer;
    [SerializeField] private CharactersContainerOnScene _charactersContainer;
    [SerializeField] private Sprite _defafultTileSprite;
    [SerializeField] private Color _defaultTileColor;
    [SerializeField] private bool GridDebug;
    [SerializeField] private bool PathfinderDebug;
    [SerializeField] private bool DrawAffectedTiles;

    private BoxCollider2D _boxCollider2D;
    private PathfinderBase _pathfinder;
    private GridGizmos _gridGizmos;

    [SerializeField] private GridSystem _grid;

    [SerializeField] private Color _gridColor = Color.white;
    [SerializeField] private Color _pathColor = Color.green;
    [SerializeField] private Color _targetingColor = Color.magenta;
    [SerializeField] private Color _targetingTilesColor = Color.red;

    [SerializeField] private GameObject _targetPathTilePrefab;

    #region init
    private void Start()
    {
        _grid = SetGrid(_width, _height, _cellSize);

        _pathfinder = new AStarPathfinder(_grid);

        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = new Vector2(_width * _cellSize / 2.0f, _height * _cellSize / 2.0f);
        _boxCollider2D.size = new Vector2(_width * _cellSize, _height * _cellSize);

        _gridGizmos = new GridGizmos(_grid);

        SetColors();
        SetPrefabs();

        if (_defafultTileSprite != null)
        {
            CreateDefaultTiles();
        }

        DrawGridGizmos();
    }

    private void CreateDefaultTiles()
    {
        for (int x =  0; x < _width; x++)
        {
            for(int  y = 0; y < _height; y++)
            {
                GameObject defaultTile = new GameObject("Default Tile");
                defaultTile.transform.parent = transform;

                SpriteRenderer renderer = defaultTile.AddComponent<SpriteRenderer>();
                renderer.sprite = _defafultTileSprite;
                renderer.color = _defaultTileColor;
                // TODO make it normal throught Layers
                renderer.sortingOrder = -1;

                defaultTile.transform.position = _grid.GetWorldPosition(x, y) 
                    + new Vector3(_grid.CellSize / 2, _grid.CellSize / 2);
            }
        }
        
    }
    #endregion

    #region properties
    public GridSystem Grid { get => _grid;  }
    public PathfinderBase Pathfinder { get => _pathfinder; }
    #endregion

    #region external interactions
    public GridSystem SetGrid(int wigth, int height, float cellSize)
        => new GridSystem(wigth, height, cellSize, transform.position,
            _environmentContainer.GetEnvironments(),
            _charactersContainer.GetCharacters());

    public List<PathfinderNodeBase> FindPath(Vector2Int start, Vector2Int target, int distanceFromTarget = 0)
    {
        List<PathfinderNodeBase> result = _pathfinder.FindPath(start, target, distanceFromTarget);

        //Debug.Log($"Path nodes count: {result.Count}");

        DrawPathGizmos(result);
        //DrawDistacneGizmos(result[result.Count - 1], _pathfinder.GetNode(target.x, target.y));
        DrawDistacneGizmos(result[result.Count - 1], _pathfinder.GetNode(target.x, target.y));
        DrawTargetingPathGizmos(result[result.Count - 1], _pathfinder.GetNode(target.x, target.y));

        return result;
    }
    public List<PathfinderNodeBase> FindPath(Vector3 start, Vector3 target, int distanceFromTarget = 0)
        => FindPath(_grid.GetNode(start).Coordinates, _grid.GetNode(target).Coordinates, distanceFromTarget);
    #endregion

    #region debug
    private void DrawGridGizmos()
    {
        if (!GridDebug) return;

        _gridGizmos.DrawGridGizmos();
    }

    private void DrawPathGizmos(List<PathfinderNodeBase> pathfinderNodes)
    {
        if (!PathfinderDebug) return;

        _gridGizmos.DrawPathGizmos(pathfinderNodes);
    }

    public void DrawDistacneGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {
        if (!PathfinderDebug) return;

        _gridGizmos.DrawDistaneGizmos(currentNode, targetNode);
    }

    public void DrawTargetingPathGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {
        if (!DrawAffectedTiles) return;

        _gridGizmos.DrawTargetingPathGizmos(currentNode, targetNode);
    }

    private void SetColors()
    {
        _gridGizmos.GridColor = _gridColor;
        _gridGizmos.PathColor = _pathColor;
        _gridGizmos.TargetingColor = _targetingColor;
        _gridGizmos.TargetingTilesColor = _targetingTilesColor;
    }

    private void SetPrefabs()
    {
        _gridGizmos.TargetPathTilePrefab = _targetPathTilePrefab;
    }
    #endregion
}
