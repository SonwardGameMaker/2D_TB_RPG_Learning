using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameGrid : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private GridSystem _grid;

    private void Start()
    {
        _grid = SetGrid(_width, _height);
    }

    public GridSystem SetGrid(int wigth, int height)
        => new GridSystem(wigth, height);
}
