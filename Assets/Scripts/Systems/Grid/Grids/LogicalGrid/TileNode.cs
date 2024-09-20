using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TileNode : NodeBase
{
    private bool _isWalkable;
    private VisionCoverStates _visionCover;
    private LogicalGrid _grid;
    private CharacterInfo _characterOnTile;
    private Environment _environmentOnTile;

    public TileNode(int x, int y, LogicalGrid grid, bool isWalkable, CharacterInfo characterOnTile) : base(x, y)
    {
        _isWalkable = isWalkable;
        _grid = grid;
        _characterOnTile = characterOnTile;
    }
    public TileNode(int xPos, int yPos, LogicalGrid grid, CharacterInfo characterOnTile) : this(xPos, yPos, grid, true, characterOnTile) { }
    public TileNode(int xPos, int yPos, LogicalGrid grid, bool isWalkable) : this(xPos, yPos, grid, isWalkable, null) { }
    public TileNode(int xPos, int yPos, LogicalGrid grid) : this(xPos, yPos, grid, true, null) { }

    #region properties
    public CharacterInfo CharacterOnTile { get => _characterOnTile; }
    public Environment EnvironmentOnTile { get => _environmentOnTile; }
    public bool IsWalkable { get => _isWalkable; }
    public Vector3 WorldPosition { get => _grid.Grid.GetWorldPosition(X, Y); }
    public Vector3 WorldPositionOfCenter { get => _grid.Grid.GetWorlPositionOfTileCenter(X, Y); }
    #endregion

    #region exteranal interactions
    public bool TrySetCharacter(CharacterInfo character)
    {
        if (character == null) throw new Exception("Chracter to set is null");

        if (!_isWalkable) return false;

        if (_characterOnTile == null)
        {
            _characterOnTile = character;
            return true;
        }
        return false;
    }

    public bool TryRemoveCharacter()
    {
        if (_characterOnTile != null)
        {
            _characterOnTile = null;
            return true;
        } 
        return false;
    }

    public bool TrySetEnvironment(Environment environment)
    {
        if (environment == null) throw new Exception("Environment to set is null");

        if (_environmentOnTile == null)
        {
            _environmentOnTile = environment;
            _isWalkable = environment.IsWalkable;
            return true;
        }
        return false;
    }

    public bool TryRemoveEnvironment()
    {
        if (_environmentOnTile != null)
        {
            _environmentOnTile = null;
            _isWalkable = true;
            return true;
        }
        return false;
    }

    public bool CanCharacerWalk(CharacterInfo character)
    {
        return _isWalkable && (_characterOnTile == null || _characterOnTile == character);
    }
    #endregion

    public override string ToString()
        => _xPos + " " + _yPos;
}
