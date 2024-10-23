using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalGrid : MonoBehaviour
{
    private GridSystem<TileNode> _grid;
    

    #region init
    public void SetUp(int width, int height, float cellSize, Vector3 originPosition,
        List<(Vector3, Environment)> environment,
        List<(Vector3, CharacterInfo)> characters)
    {

        _grid = new GridSystem<TileNode>(width, height, cellSize, originPosition,
            (int x, int y) => new TileNode(x, y, this));

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.offset = new Vector2(width * cellSize / 2.0f, height * cellSize / 2.0f);
        boxCollider2D.size = new Vector2(width * cellSize, height * cellSize);

        InitGridEnvironment(environment);
        InitCharactersOnGrid(characters);
    }
    private void InitGridEnvironment(List<(Vector3, Environment)> environment)
    {
        if (environment == null) Debug.LogError("Environment is null");
        if (environment.Count == 0) Debug.LogWarning("Environment is empty");

        foreach ((Vector3, Environment) env in environment)
        {
            _grid.GetNode(env.Item1).TrySetEnvironment(env.Item2);
        }
    }
    private void InitCharactersOnGrid(List<(Vector3, CharacterInfo)> characters)
    {
        if (characters == null) Debug.LogError("Characters is null");
        if (characters.Count == 0) Debug.LogWarning("Characters is empty");

        foreach ((Vector3, CharacterInfo) character in characters)
        {
            _grid.GetNode(character.Item1).TrySetCharacter(character.Item2);
        }
    }
    #endregion

    #region properties
    public GridSystem<TileNode> Grid { get => _grid; }
    #endregion

    #region external interactions

    public bool MoveChararacter(Vector3 characterPosition, Vector3 targetPosition)
        => MoveChararacter(_grid.GetPositionOnGrid(characterPosition), _grid.GetPositionOnGrid(targetPosition));
    public bool MoveChararacter(Vector2Int characterPosition, Vector2Int targetPosition)
    {
        TileNode characterTile = _grid.GetNode(characterPosition.x, characterPosition.y);

        if (characterTile.CharacterOnTile == null)
            throw new Exception($"No characters on tile {characterTile.X}, {characterTile.Y}");
        //return false;

        if (_grid.GetNode(targetPosition.x, targetPosition.y).TrySetCharacter(characterTile.CharacterOnTile))
        {
            characterTile.TryRemoveCharacter();
            return true;
        }
        return false;
    }
    #endregion

}
