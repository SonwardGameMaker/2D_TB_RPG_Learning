using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TileNode
{
    protected int _xPos;
    protected int _yPos;
    private bool _isWalkable;
    private VisionCoverStates _visionCover;
    private GridSystem _grid;
    private CharacterInfo _characterOnTile;
    private Environment _environmentOnTile;

    public TileNode(int x, int y, bool isWalkable, CharacterInfo characterOnTile, GridSystem grid)
    {
        _xPos = x;
        _yPos = y;
        _isWalkable = isWalkable;
        _grid = grid;
        _characterOnTile = characterOnTile;
    }
    public TileNode(int xPos, int yPos, CharacterInfo characterOnTile, GridSystem grid) : this(xPos, yPos, true, characterOnTile, grid) { }
    public TileNode(int xPos, int yPos, bool isWalkable, GridSystem grid) : this(xPos, yPos, isWalkable, null, grid) { }
    public TileNode(int xPos, int yPos, GridSystem grid) : this(xPos, yPos, true, null, grid) { }

    #region properties
    public int X { get => _xPos; }
    public int Y { get => _yPos; }
    public Vector2Int Coordinates { get => new Vector2Int(_xPos, _yPos); }
    public CharacterInfo CharacterOnTile { get => _characterOnTile; }
    public Environment EnvironmentOnTile { get => _environmentOnTile; }
    public bool IsWalkable { get => _isWalkable; }
    public Vector3 WorldPosition { get => _grid.GetWorldPosition(X, Y); }
    public Vector3 WorldPositionOfCenter { get => _grid.GetWorlPositionOfTileCenter(X, Y); }
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
