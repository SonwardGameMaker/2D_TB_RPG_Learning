using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGridNode : NodeBase
{
    private SpriteRenderer _tileSprite;

    public VisualGridNode(int x, int y, SpriteRenderer sprite) : base(x, y)
    {
        _tileSprite = sprite;
    }

    public SpriteRenderer TileSprite { get => _tileSprite;  }
}
