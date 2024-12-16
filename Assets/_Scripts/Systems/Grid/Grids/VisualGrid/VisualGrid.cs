using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGrid : MonoBehaviour
{
    private GridSystem<VisualGridNode> _grid;

    [SerializeField] private Sprite _defafultTileSprite;
    [SerializeField] private Color _defaultTileColor;

    #region init
    public void Setup(int width, int height, float cellSize, Vector3 originPosition)
    {
        _grid = new GridSystem<VisualGridNode>(width, height, cellSize, originPosition, CreateVisualNode);
        SetPositions();
    }

    private VisualGridNode CreateVisualNode(int x, int y)
    {
        if (_defafultTileSprite == null) throw new Exception("Default sprite is null");

        GameObject defaultTile = new GameObject("Default Tile");
        defaultTile.transform.parent = transform;

        SpriteRenderer renderer = defaultTile.AddComponent<SpriteRenderer>();
        renderer.sprite = _defafultTileSprite;
        renderer.color = _defaultTileColor;
        // TODO make it normal throught Layers
        renderer.sortingOrder = -1;

        //defaultTile.transform.position = _grid.GetWorldPosition(x, y)
        //    + new Vector3(_grid.CellSize / 2, _grid.CellSize / 2);

        return new VisualGridNode(x, y, renderer);
    }

    private void SetPositions()
    {
        for (int x = 0; x < _grid.Width; x++)
        {
            for (int y = 0; y < _grid.Height; y++)
            {
                _grid.GetNode(x, y).TileSprite.transform.position = _grid.GetWorldPosition(x, y)
            + new Vector3(_grid.CellSize / 2, _grid.CellSize / 2);
            }
        }
    }
    #endregion

    public GridSystem<VisualGridNode> Grid { get => _grid; }
}
