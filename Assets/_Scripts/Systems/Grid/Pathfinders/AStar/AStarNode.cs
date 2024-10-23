using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode : PathfinderNodeBase
{
    public int gCost;
    public int hCost;
    public int fCost;

    public AStarNode(int x, int y, TileNode targetNode) : base(x, y, targetNode) { }

    public void CalculateFCost()
        => fCost = gCost + hCost;
}
