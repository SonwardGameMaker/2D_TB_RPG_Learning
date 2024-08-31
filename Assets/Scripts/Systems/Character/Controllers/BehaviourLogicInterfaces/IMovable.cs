using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    public void StartMove(List<(Vector3, TileNode)> path);
}
