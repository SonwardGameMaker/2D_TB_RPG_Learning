using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MovableContainer : PlayerBehaviourScriptContainer
{
    private Movable _movable;

    public MovableContainer(Movable movable)
    {
        _movable = movable;
    }

    public override void Activate()
    {
        // TODO Create MovingState and make this method to redirect on it
        throw new System.NotImplementedException();
    }
}
