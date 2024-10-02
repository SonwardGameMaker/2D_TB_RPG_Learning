using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AStarPathfinder : PathfinderBase
{
    private const int MOVE_STRAINGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private PathfinderGridSystem<AStarNode> _pathGrid;
    private List<AStarNode> _openList;
    private List<AStarNode> _closedList;

    public AStarPathfinder(LogicalGrid targetGrid)
    {
        _pathGrid = new PathfinderGridSystem<AStarNode>(targetGrid, (int x, int y, TileNode tn) => new AStarNode(x, y, tn));
    }

    #region eternal calculations
    public override List<PathfinderNodeBase> FindPath(
        Vector2Int startNodeCoord,
        Vector2Int targetNodeCoord,
        List<Vector2> ignoringNodes = null,
        int distanceFromTarget = 0)
    {
        AStarNode startNode = _pathGrid.GetNode(startNodeCoord.x, startNodeCoord.y);
        AStarNode targetNode = _pathGrid.GetNode(targetNodeCoord.x, targetNodeCoord.y);

        if (ignoringNodes == null)
            ignoringNodes = new List<Vector2>();

        CharacterInfo character = startNode.TargetNode.CharacterOnTile;

        // TODO maybe change it
        if (!targetNode.IsWalkable && !ignoringNodes.Contains(targetNodeCoord))
            if (distanceFromTarget <= 0)
                distanceFromTarget = 1;

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
        startNode.hCost = CalculateMoveDistance(startNode, targetNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            AStarNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == targetNode
                || (CalculateDistance(currentNode, targetNode) <= distanceFromTarget)
                && CheckLineOfSight(currentNode, targetNode))
                return CalculatePath(currentNode);

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach (AStarNode neigbourNode in GetNeighbourList(currentNode))
            {
                if (_closedList.Contains(neigbourNode)
                    || !CanDiagonalMove(currentNode, neigbourNode, ignoringNodes))
                        continue; 

                if (!neigbourNode.IsWalkable
                    && !ignoringNodes.Contains(neigbourNode.Coordinates)
                    && neigbourNode != targetNode)
                {
                    _closedList.Add(neigbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateMoveDistance(currentNode, neigbourNode);
                if (tentativeGCost  < neigbourNode.gCost)
                {
                    neigbourNode.CameFromNode = currentNode;
                    neigbourNode.gCost = tentativeGCost;
                    neigbourNode.hCost = CalculateMoveDistance(neigbourNode, targetNode);
                    neigbourNode.CalculateFCost();

                    if (!_openList.Contains(neigbourNode))
                        _openList.Add(neigbourNode);
                }
            }
        }

        return null;
    }

    public override PathfinderNodeBase GetNode(int x, int y)
        => _pathGrid.GetNode(x, y);
    #endregion

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

    private int CalculateMoveDistance(AStarNode a, AStarNode b)
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

    private bool CanDiagonalMove(AStarNode tile, AStarNode neigbour, List<Vector2> ignoringNodes)
    {
        int xDiff = neigbour.X - tile.X;
        int yDiff = neigbour.Y - tile.Y;

        if (xDiff != 0 && yDiff != 0)
        {
            bool xWalkable = _pathGrid.GetNode(tile.X + xDiff, tile.Y).IsWalkable 
                || ignoringNodes.Contains(new Vector2(tile.X + xDiff, tile.Y));
            bool yWalkable = _pathGrid.GetNode(tile.X, tile.Y + yDiff).IsWalkable 
                || ignoringNodes.Contains(new Vector2(tile.X, tile.Y + yDiff));
            
            if (!xWalkable || !yWalkable) return false;
            else return true;
        }
        return true;
    }

    private int CalculateDistance(AStarNode a, AStarNode b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);

        int result = (int)Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);

        //Debug.Log($"Distance: {result}");

        return result;
    }

    private bool CheckLineOfSight(AStarNode a, AStarNode b)
    {
        List<AStarNode> cells = General.GetLineCells(new Vector2Int(a.X, a.Y), new Vector2Int(b.X, b.Y))
            .Select(cr => _pathGrid.GetNode(cr.x, cr.y)).ToList();

        foreach (AStarNode cell in cells)
            if (!CheckCellOnLineOfSight(cell))
                return false;
        return true;
    }

    private bool CheckCellOnLineOfSight(AStarNode a)
        => a.TargetNode.EnvironmentOnTile == null 
        || a.TargetNode.EnvironmentOnTile.ThroughtShootable;
    #endregion
}
