using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private LogicalGrid _logicalGrid;
    [SerializeField] private VisualGrid _visualGrid;
    [SerializeField] private GridGizmos _debugGid;
    
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private EnvironmentContainer _environmentContainer;
    [SerializeField] private CharactersContainerOnScene _charactersContainer;

    private PathfinderBase _pathfinder;

    #region init
    public void Setup()
    {
        List<(Vector3, CharacterInfo)> characters = _charactersContainer.GetCharacters();
        foreach (var character in characters)
        {
            character.Item2.Setup();
            if (character.Item2.TryGetComponent(out CharacterIngameController controller))
                controller.GridManager = this;
        }

        _logicalGrid.Setup(_width, _height, _cellSize, transform.position,
            _environmentContainer.GetEnvironments(),
            _charactersContainer.GetCharacters());

        _pathfinder = new AStarPathfinder(_logicalGrid);

        _visualGrid.Setup(_width, _height, _cellSize, transform.position);

        _debugGid.TargetGrid = _logicalGrid;

        DrawGridGizmos();
    }
    #endregion

    #region properties
    public LogicalGrid Grid { get => _logicalGrid;  }
    public PathfinderBase Pathfinder { get => _pathfinder; }
    #endregion

    #region external interactions
    public List<PathfinderNodeBase> FindPath(Vector2Int start, Vector2Int target, List<Vector2> ignoringNodes, int distanceFromTarget = 0)
    {

        List<PathfinderNodeBase> result = _pathfinder.FindPath(start, target, ignoringNodes, distanceFromTarget);

        DrawPathGizmos(result);
        DrawDistacneGizmos(result[result.Count - 1], _pathfinder.GetNode(target.x, target.y));
        DrawTargetingPathGizmos(result[result.Count - 1], _pathfinder.GetNode(target.x, target.y));

        return result;
    }
    public List<PathfinderNodeBase> FindPath(Vector3 start, Vector3 target, List<Vector2> ignoringNodes, int distanceFromTarget = 0)
        => FindPath(_logicalGrid.Grid.GetNode(start).Coordinates, _logicalGrid.Grid.GetNode(target).Coordinates, ignoringNodes, distanceFromTarget);
    #endregion

    #region debug
    private void DrawGridGizmos()
    {
        _debugGid.DrawGridGizmos();
    }

    private void DrawPathGizmos(List<PathfinderNodeBase> pathfinderNodes)
    {
        _debugGid.DrawPathGizmos(pathfinderNodes);
    }

    public void DrawDistacneGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {

        _debugGid.DrawDistaneGizmos(currentNode, targetNode);
    }

    public void DrawTargetingPathGizmos(PathfinderNodeBase currentNode, PathfinderNodeBase targetNode)
    {

        _debugGid.DrawTargetingPathGizmos(currentNode, targetNode);
    }
    #endregion
}
