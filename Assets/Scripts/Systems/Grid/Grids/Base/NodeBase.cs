using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBase
{
    protected int _xPos;
    protected int _yPos;

    public NodeBase(int x, int y)
    {
        _xPos = x;
        _yPos = y;
    }

    public int X { get => _xPos; }
    public int Y { get => _yPos; }
    public Vector2Int Coordinates { get => new Vector2Int(_xPos, _yPos); }
}
