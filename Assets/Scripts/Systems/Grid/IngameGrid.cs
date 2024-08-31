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
    [SerializeField] private bool GridDebug;
    [SerializeField] private bool PathfinderDebug;

    private BoxCollider2D _boxCollider2D;
    private PathfinderBase _pathfinder;
    private GridGizmos _gridGizmos;

    [SerializeField] private GridSystem _grid;

    private void Start()
    {
        _grid = SetGrid(_width, _height, _cellSize);

        _pathfinder = new AStarPathfinder(_grid);

        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = new Vector2(_width * _cellSize / 2.0f, _height * _cellSize / 2.0f);
        _boxCollider2D.size = new Vector2(_width * _cellSize, _height * _cellSize);

        _gridGizmos = new GridGizmos(_grid);

        DrawGridGizmos();
    }

    #region properties
    public GridSystem Grid { get => _grid;  }
    public PathfinderBase Pathfinder { get => _pathfinder; }
    #endregion

    #region external interactions
    public GridSystem SetGrid(int wigth, int height, float cellSize)
        => new GridSystem(wigth, height, cellSize, transform.position,
            _environmentContainer.GetEnvironments(),
            _charactersContainer.GetCharacters());

    public List<PathfinderNodeBase> FindPath(Vector2Int start, Vector2Int target)
    {
        List<PathfinderNodeBase> result = _pathfinder.FindPath(start, target);

        DrawPathGizmos(result);

        return result;
    }
    public List<PathfinderNodeBase> FindPath(Vector3 start, Vector3 target)
        => FindPath(_grid.GetNode(start).Coordinates, _grid.GetNode(target).Coordinates);
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
    #endregion
}
