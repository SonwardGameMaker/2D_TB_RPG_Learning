using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfinderNodeBase : NodeBase
{
    protected TileNode _targetNode;
    protected bool _isWalkable;

    public PathfinderNodeBase(int x, int y, TileNode targetNode) : base(x, y)
    {
        _targetNode = targetNode;
        UpdateWalkableInfo();
        targetNode.OnDataChanged += UpdateWalkableInfo;
    }

    #region properties
    public TileNode TargetNode { get => _targetNode; }
    public bool IsWalkable { get =>  _isWalkable; }
    public PathfinderNodeBase CameFromNode { get; set; }
    public int CameFromCost { get; set; }
    #endregion

    public override string ToString()
        => _xPos + " " + _yPos;

    #region internal operations
    protected void UpdateWalkableInfo()
    {
        _isWalkable = _targetNode.IsWalkable && _targetNode.CharacterOnTile == null;
    }
    #endregion
}
