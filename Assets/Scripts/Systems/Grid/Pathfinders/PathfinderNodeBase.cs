using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfinderNodeBase
{
    protected int _xPos;
    protected int _yPos;

    protected TileNode _targetNode;
    
    public PathfinderNodeBase(int x, int y, TileNode targetNode)
    {
        _xPos = x;
        _yPos = y;
        _targetNode = targetNode;
    }

    #region properties
    public int X { get => _xPos;  }
    public int Y { get => _yPos; }
    public Vector2Int Coordinates { get => new Vector2Int(_xPos, _yPos); }
    public TileNode TargetNode { get => _targetNode; }
    public PathfinderNodeBase CameFromNode { get; set; }
    #endregion

    public override string ToString()
        => _xPos + " " + _yPos;
}
