using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder : PathfinderBase
{
    private const int MOVE_STRAINGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private PathfinderGridSystem<AStarNode> _pathGrid;
    private List<AStarNode> _openList;
    private List<AStarNode> _closedList;

    public AStarPathfinder(GridSystem targetGrid)
    {
        _pathGrid = new PathfinderGridSystem<AStarNode>(targetGrid, (int x, int y, TileNode tn) => new AStarNode(x, y, tn));
    }

    public override List<PathfinderNodeBase> FindPath(Vector2Int startNodeCoord, Vector2Int targetNodeCoord)
    {
        AStarNode startNode = _pathGrid.GetNode(startNodeCoord.x, startNodeCoord.y);
        AStarNode targetNode = _pathGrid.GetNode(targetNodeCoord.x, targetNodeCoord.y);

        //if (!targetNode.TargetNode.IsWalkable) return null;

        _openList = new List<AStarNode>() { startNode };
        _closedList = new List<AStarNode>();

        for (int x = 0; x < _pathGrid.Width; x++)
        {
            for (int y = 0;  y < _pathGrid.Height; y++)
            {
                AStarNode aStarNode = _pathGrid.GetNode(x, y);
                aStarNode.gCost = int.MaxValue;
                aStarNode.CalculateFCost();
                aStarNode.CameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, targetNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            AStarNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == targetNode)
                return CalculatePath(targetNode);

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach (AStarNode neigbourNode in GetNeighbourList(currentNode))
            {
                if (_closedList.Contains(neigbourNode)) continue;
                if (!neigbourNode.TargetNode.IsWalkable)
                {
                    _closedList.Add(neigbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neigbourNode);
                if (tentativeGCost  < neigbourNode.gCost)
                {
                    neigbourNode.CameFromNode = currentNode;
                    neigbourNode.gCost = tentativeGCost;
                    neigbourNode.hCost = CalculateDistance(neigbourNode, targetNode);
                    neigbourNode.CalculateFCost();

                    if (!_openList.Contains(neigbourNode))
                        _openList.Add(neigbourNode);
                }
            }
        }

        return null;
    }

    #region internal calcultions
    private List<PathfinderNodeBase> CalculatePath(AStarNode endNode)
    {
        List<PathfinderNodeBase> result = new List<PathfinderNodeBase>();
        result.Add(endNode);
        PathfinderNodeBase currentNode = endNode;
        while (currentNode.CameFromNode != null)
        {
            currentNode.CameFromCost = CalculateCameFromCost(currentNode.CameFromNode, currentNode);
            result.Add(currentNode.CameFromNode);
            currentNode = currentNode.CameFromNode;
        }
        result.Reverse();
        return result;
    }
    private int CalculateCameFromCost(PathfinderNodeBase cameFromNode, PathfinderNodeBase targetNode)
    {
        int relativeX = targetNode.X - cameFromNode.X;
        int relativeY = targetNode.Y - cameFromNode.Y;

        if (relativeX != 0 && relativeY != 0)
            return MOVE_DIAGONAL_COST;
        else if (relativeX == 0 && relativeY == 0)
            return 0;
        else
            return MOVE_STRAINGHT_COST;
    }

    private int CalculateDistance(AStarNode a, AStarNode b)
    {
        int xDistacne = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remaining = Mathf.Abs(xDistacne - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistacne, yDistance) + MOVE_STRAINGHT_COST * remaining;
    }

    private AStarNode GetLowestFCostNode(List<AStarNode> pathNodeList)
    {
        AStarNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = pathNodeList[i];
        return lowestFCostNode;
    }

    private List<AStarNode> GetNeighbourList(AStarNode node)
    {
        List<AStarNode> result = new List<AStarNode>();
        for (int x = node.X - 1; x < node.X + 2; x++)
        {
            for (int y = node.Y - 1; y < node.Y + 2; y++)
            {
                if (x >= 0 && x < _pathGrid.Width
                    && y >= 0 && y < _pathGrid.Height
                    && !(x == node.X && y == node.Y))
                    result.Add(_pathGrid.GetNode(x, y));
            }
        }
        return result;
    }
    #endregion
}
