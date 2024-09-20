using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    public void Setup(CharacterInfo characterInfo);
    public void Rotate(Vector3 targetPosition, Action onEndCoroutineAction);
    public void Move(List<TileNode> path, Action onEndCoroutineAction);
}
