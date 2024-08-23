using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameGrid : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;

    private BoxCollider2D _boxCollider2D;

    private GridSystem<TileNode> _grid;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _grid = SetGrid(_width, _height, _cellSize);

        _boxCollider2D.offset = new Vector2(_width * _cellSize / 2.0f, _height * _cellSize / 2.0f);
        _boxCollider2D.size = new Vector2(_width * _cellSize, _height * _cellSize);
    }

    #region properties
    public GridSystem<TileNode> Grid { get => _grid;  }
    #endregion

    #region external interactions
    public GridSystem<TileNode> SetGrid(int wigth, int height, float cellSize)
        => new GridSystem<TileNode>(wigth, height, cellSize, transform.position, (int x, int y) => new TileNode(x, y));
    #endregion
}
