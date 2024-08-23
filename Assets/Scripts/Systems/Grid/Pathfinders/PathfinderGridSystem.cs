using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderGridSystem<TGridObjet> where TGridObjet : PathfinderNodeBase
{
    #region fields
    private int _width;
    private int _height;
    private TGridObjet[,] _grid;
    private GridSystem _targetGrid;
    #endregion

    #region init
    public PathfinderGridSystem(GridSystem targetGrid, Func<int, int, TileNode, TGridObjet> CreateGridNode)
    {
        _targetGrid = targetGrid;
        _width = targetGrid.Width;
        _height = targetGrid.Height;
        _grid = CreateGrid(_width, _height, CreateGridNode);

        TGridObjet[,] CreateGrid(int width, int height, Func<int, int, TileNode, TGridObjet> CreateGridNode)
        {
            TGridObjet[,] result = new TGridObjet[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = CreateGridNode(x, y, _targetGrid.GetNode(x, y));
                }
            }

            return result;
        }
    }
    #endregion

    #region properties
    public int Width { get => _width; }
    public int Height { get => _height; }
    #endregion

    #region external interactions
    public TGridObjet GetNode(int x, int y)
        => _grid[x, y];
    #endregion
}
