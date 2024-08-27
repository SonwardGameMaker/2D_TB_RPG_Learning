using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameGrid : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private bool GridDebug;
    [SerializeField] private bool PathfinderDebud;

    private BoxCollider2D _boxCollider2D;
    //private PathfinderBase _pathfinder;

    [SerializeField] private GridSystem _grid;

    private void Start()
    {
        _grid = SetGrid(_width, _height, _cellSize);
        _grid.GridDebug = GridDebug;

        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = new Vector2(_width * _cellSize / 2.0f, _height * _cellSize / 2.0f);
        _boxCollider2D.size = new Vector2(_width * _cellSize, _height * _cellSize);
    }

    #region properties
    public GridSystem Grid { get => _grid;  }
    #endregion

    #region external interactions
    public GridSystem SetGrid(int wigth, int height, float cellSize)
        => new GridSystem(wigth, height, cellSize, transform.position);

    public List<PathfinderNodeBase> FindPath(Vector2Int start, Vector2Int target)
        => _grid.FindPath(start, target);
    public List<PathfinderNodeBase> FindPath(Vector3 start, Vector3 target)
        => _grid.FindPath(start, target);
    #endregion
}
