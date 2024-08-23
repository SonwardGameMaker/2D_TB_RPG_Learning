using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode
{
    protected int _xPos;
    protected int _yPos;
    private bool _isWalkable;
    private CharacterInfo _characterOnTile;

    public TileNode(int x, int y, bool isWalkable, CharacterInfo characterOnTile)
    {
        _xPos = x;
        _yPos = y;
        _isWalkable = isWalkable;
        _characterOnTile = characterOnTile;
    }
    public TileNode(int xPos, int yPos, CharacterInfo characterOnTile) : this(xPos, yPos, true, characterOnTile) { }
    public TileNode(int xPos, int yPos, bool isWalkable) : this(xPos, yPos, isWalkable, null) { }
    public TileNode(int xPos, int yPos) : this(xPos, yPos, true, null) { }

    #region properties
    public int X { get => _xPos; }
    public int Y { get => _yPos; }
    public Vector2Int Coordinates { get => new Vector2Int(_xPos, _yPos); }
    public CharacterInfo CharacterOnTile { get => _characterOnTile; }
    public bool IsWalkable { get => _isWalkable; }
    #endregion

    #region exteranal interactions
    public bool TrySetCharacter(CharacterInfo character)
    {
        if (character == null) throw new Exception("Chracter to set is null");

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
    #endregion

    public override string ToString()
        => _xPos + " " + _yPos;
}