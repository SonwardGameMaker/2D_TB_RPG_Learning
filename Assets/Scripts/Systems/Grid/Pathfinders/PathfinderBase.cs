using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfinderBase
{
    public abstract List<PathfinderNodeBase> FindPath(Vector2Int startNodeCoord, Vector2Int targetNodeCoord, List<Vector2> ignoringNodes = null, int distanceFromTaret = 0);
    public abstract PathfinderNodeBase GetNode(int x, int y);
}
