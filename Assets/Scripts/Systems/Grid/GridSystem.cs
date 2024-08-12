using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int _width;
    private int _height;
    private Node[,] _grid;

    public GridSystem(int width, int height)
    {
        _width = width;
        _height = height;
        _grid = new Node[width, height];
    }
}
